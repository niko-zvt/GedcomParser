using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class ReligionParser
    {
        internal static Religion ParseReligion(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var religion = new Religion();
            religion.Id = incomingChunk.Id;
            religion.Name = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        religion.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        religion.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        religion.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        religion.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Religion Type='{chunk.Type}'");
                        break;
                }
            }

            return religion;
        }
    }
}
