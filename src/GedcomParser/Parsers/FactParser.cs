using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class FactParser
    {
        internal static Fact ParseFact(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var fact = new Fact();
            fact.Id = incomingChunk.Id;
            fact.Text = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        fact.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        fact.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "_PLC":
                        fact.PlaceId = chunk.Reference;
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Fact Type='{chunk.Type}'");
                        break;
                }
            }

            return fact;
        }
    }
}
