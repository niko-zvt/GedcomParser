using System.Linq;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class ConfirmationParser
    {
        internal static Confirmation ParseConfirmation(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var confirmation = new Confirmation();
            confirmation.Id = incomingChunk.Id;
            confirmation.Description = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "CONC":
                        confirmation.Description = confirmation.Description + " " + chunk.Data;
                        break;

                    case "CONT":
                        confirmation.Description = confirmation.Description + "\n" + chunk.Data;
                        break;

                    case "PLAC":
                    case "DATE":
                        confirmation.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "NOTE":
                        confirmation.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "TYPE":
                        confirmation.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        confirmation.Citations.Add(resultContainer.ParseCitation(chunk));
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
