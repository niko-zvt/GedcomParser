using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class ReferenceParser
    {
        internal static Reference ParseReference(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var reference = new Reference();
            reference.Id = incomingChunk.Id;
            reference.Text = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "TYPE":
                        reference.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Reference Type='{chunk.Type}'");
                        break;
                }
            }

            return reference;
        }
    }
}
