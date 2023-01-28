using System;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class DeathParser
    {
        internal static Death ParseDeath(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var death = new Death();
            death.Id = incomingChunk.Id;
            death.Name = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NOTE":
                        death.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "AGE":
                        death.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TYPE":
                        death.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ADDR":
                        death.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "_PLC":
                        death.PlaceId = chunk.Reference;
                        break;

                    case "SOUR":
                        death.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "OBJE":
                        death.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "AGNC":
                        death.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CAUS":
                        death.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "PLAC":
                    case "DATE":
                        death.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"DeathParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            return death;
        }
    }
}