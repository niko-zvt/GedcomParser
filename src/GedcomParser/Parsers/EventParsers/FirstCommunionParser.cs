using System.Linq;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class FirstCommunionParser
    {
        internal static FirstCommunion ParseFirstCommunion(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var firstCommunion = new FirstCommunion();
            firstCommunion.Id = incomingChunk.Id;
            firstCommunion.Description = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "CONC":
                        firstCommunion.Description = firstCommunion.Description + " " + chunk.Data;
                        break;

                    case "CONT":
                        firstCommunion.Description = firstCommunion.Description + "\n" + chunk.Data;
                        break;

                    case "PLAC":
                    case "DATE":
                        firstCommunion.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "NOTE":
                        firstCommunion.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "TYPE":
                        firstCommunion.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        firstCommunion.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle First Communion Type='{chunk.Type}'");
                        break;
                }
            }

            return firstCommunion;
        }
    }
}
