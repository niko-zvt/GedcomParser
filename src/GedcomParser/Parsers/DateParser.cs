using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class DateParser
    {
        internal static DatePlace ParseDatePlace(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var datePlace = new DatePlace();
            datePlace.Description = indiChunk.Data;

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "DATE":
                        datePlace.Date = chunk.Data;
                        break;

                    case "PLAC":
                        datePlace.Place = chunk.Data;
                        break;

                    case "MAP":
                        var map = chunk;
                        if (map != null)
                        {
                            datePlace.Latitude = map.SubChunks.SingleOrDefault(c => c.Type == "LATI")?.Data;
                            datePlace.Longitude = map.SubChunks.SingleOrDefault(c => c.Type == "LONG")?.Data;
                        }
                        break;

                    case "NOTE":
                        datePlace.Note = resultContainer.ParseNote(chunk.Data, chunk);
                        break;

                    case "TYPE":
                        datePlace.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        datePlace.Citation = resultContainer.ParseCitation(chunk);
                        break;

                    case "TEXT":
                        datePlace.Description = resultContainer.ParseText(datePlace.Description + chunk.Data, chunk);
                        break;

                    case "ADDR":
                        datePlace.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "CAUS":
                        datePlace.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "EMAIL":
                        datePlace.Address.Email.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "AGE":
                        datePlace.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "OBJE":
                        datePlace.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "_PLC":
                        datePlace.PlaceId = chunk.Reference;
                        break;

                    case "PHON":
                        datePlace.Address.Phone.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Date Place Type='{chunk.Type}'");
                        break;
                }
            }

            return datePlace;
        }

        internal static string ParseDateTime(this GedcomChunk chunk)
        {
            return (chunk.SubChunks.SingleOrDefault(c => c.Type == "DATE")?.Data + " " + chunk.SubChunks.SingleOrDefault(c => c.Type == "TIME")?.Data).Trim();
        }
    }
}