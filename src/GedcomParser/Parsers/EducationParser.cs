using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class EducationParser
    {
        internal static Education ParseEducation(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var education = new Education();
            education.Id = incomingChunk.Id;
            education.Title = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        education.Date = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "NOTE":
                        education.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "TYPE":
                        education.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        education.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Education Type='{chunk.Type}'");
                        break;
                }
            }

            return education;
        }
    }
}
