using System.Linq;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class NationalityParser
    {
        internal static Nationality ParseNationality(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var nationality = new Nationality();
            nationality.Id = incomingChunk.Id;
            nationality.Name = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        nationality.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        nationality.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        nationality.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        nationality.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Nationality Type='{chunk.Type}'");
                        break;
                }
            }

            return nationality;
        }
    }
}
