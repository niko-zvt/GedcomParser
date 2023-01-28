using GedcomParser.Entities;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers.EventParsers
{
    public static class ResidenceParser
    {
        internal static Residence ParseResidence(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var parentType = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";

            var residence = new Residence();
            residence.Id = incomingChunk.Id;
            residence.Name = incomingChunk.Data;
            residence.Type = incomingChunk.Type;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        residence.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "DSCR":
                        residence.Description = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ROLE":
                        residence.Role = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TYPE":
                        residence.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        residence.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "NOTE":
                        residence.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OBJE":
                        residence.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "ADDR":
                        residence.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "AGE":
                        residence.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "AGNC":
                        residence.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CAUS":
                        residence.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "PHON":
                        residence.DatePlace.Address.Phone.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "EMAIL":
                        residence.DatePlace.Address.Email.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "_PLC":
                        residence.PlaceId = chunk.Reference;
                        break;

                    default:
                        resultContainer.Errors.Add($"ResidenceParser: Failed to handle '{parentType}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }
            return residence;
        }
    }
}