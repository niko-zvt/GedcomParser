using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class ConfirmationParser
    {
        internal static Confirmation ParseConfirmation(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var confirmation = new Confirmation();
            confirmation.Id = incomingChunk.Id;
            confirmation.Title = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        confirmation.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "NOTE":
                        confirmation.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Confirmation Type='{chunk.Type}'");
                        break;
                }
            }

            return confirmation;
        }
    }
}
