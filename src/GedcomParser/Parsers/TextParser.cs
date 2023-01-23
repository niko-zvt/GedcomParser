using System;
using System.Text;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;


namespace GedcomParser.Parsers
{
    public static class TextParser
    {
        internal static string ParseText(this ResultContainer resultContainer, string previousText, GedcomChunk incomingChunk)
        {
            var textChunk = incomingChunk;
            if (incomingChunk.Reference.IsSpecified())
            {
                textChunk = resultContainer.GetIdChunk(textChunk.Reference);
                if (textChunk == null)
                {
                    throw new Exception($"Unable to find Text with Id='{incomingChunk.Reference}'");
                }
            }

            var sb = new StringBuilder();
            foreach (var chunk in textChunk.SubChunks)
            {
                if (chunk.IsUnwantedBlob())
                {
                    sb.AppendLine("(Skipped blob content)");
                    break;
                }

                switch (chunk.Type)
                {
                    case "CONC":
                        sb.Append(" " + chunk.Data);
                        break;

                    case "CONT":
                        sb.AppendLine(chunk.Data);
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : null;
                        resultContainer.Errors.Add($"TextParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            var resultText = previousText.IsSpecified() ? previousText + Environment.NewLine + sb : sb.ToString();

            return resultText.TrimEnd(new char[] { '\r', '\n' });
        }

        private static bool IsUnwantedBlob(this GedcomChunk chunk)
        {
            // TODO: We should probably make this check more intelligent 
            return chunk.Data?.Contains("<span") ?? false;
        }
    }
}