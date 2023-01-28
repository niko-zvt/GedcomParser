using GedcomParser.Entities;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers.EventParsers
{
    public static class EventParser
    {
        internal static Event ParseEvent(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var parentType = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";

            var currentEvent = new Event();
            currentEvent.Id = incomingChunk.Id;
            currentEvent.Name = incomingChunk.Data;
            currentEvent.Type = incomingChunk.Type;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        currentEvent.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "DSCR":
                        currentEvent.Description = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ROLE":
                        currentEvent.Role = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TYPE":
                        currentEvent.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        currentEvent.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "NOTE":
                        currentEvent.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OBJE":
                        currentEvent.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "ADDR":
                        currentEvent.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "AGE":
                        currentEvent.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "AGNC":
                        currentEvent.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CAUS":
                        currentEvent.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "PHON":
                        currentEvent.DatePlace.Address.Phone.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "EMAIL":
                        currentEvent.DatePlace.Address.Email.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "_PLC":
                        currentEvent.PlaceId = chunk.Reference;
                        break;

                    default:
                        resultContainer.Errors.Add($"EventParser: Failed to handle '{parentType}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }
            return currentEvent;
        }
    }
}