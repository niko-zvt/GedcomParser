using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class IdentifierParser
    {
        internal static Identifier ParseIdentifier(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var identifier = new Identifier();
            identifier.Id = incomingChunk.Id;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        identifier.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        identifier.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        identifier.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        identifier.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Identifier Type='{chunk.Type}'");
                        break;
                }
            }

            return identifier;
        }
    }
}
