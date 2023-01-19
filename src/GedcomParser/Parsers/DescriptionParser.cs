using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class DescriptionParser
    {
        internal static Description ParseDescription(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var description = new Description();
            description.Id = incomingChunk.Id;
            description.Text = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        description.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        description.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "_PLC":
                        description.PlaceId = chunk.Reference;
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Fact Type='{chunk.Type}'");
                        break;
                }
            }

            return description;
        }
    }
}
