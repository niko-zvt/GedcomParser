using System.Linq;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class EducationParser
    {
        internal static Education ParseEducation(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var education = new Education();
            education.Id = incomingChunk.Id;
            education.Description = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "CONC":
                        education.Description = education.Description + " " + chunk.Data;
                        break;

                    case "CONT":
                        education.Description = education.Description + "\n" + chunk.Data;
                        break;

                    case "PLAC":
                    case "DATE":
                        education.DatePlace = resultContainer.ParseDatePlace(chunk);
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
