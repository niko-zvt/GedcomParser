using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class OccupationParser
    {
        internal static Occupation ParseOccupation(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var occupation = new Occupation();
            occupation.Id = incomingChunk.Id;
            occupation.Title = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        occupation.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "NOTE":
                        occupation.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "SOUR":
                        occupation.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "_PLC":
                        occupation.PlaceId = chunk.Reference;
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Occupation Type='{chunk.Type}'");
                        break;
                }
            }

            return occupation;
        }
    }
}
