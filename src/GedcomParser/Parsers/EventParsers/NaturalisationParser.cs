using GedcomParser.Entities;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;

namespace GedcomParser.Parsers.EventParsers
{
    public static class NaturalisationParser
    {
        internal static Naturalisation ParseNaturalisation(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var parentType = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";

            var naturalisation = new Naturalisation();
            naturalisation.Id = incomingChunk.Id;
            naturalisation.Name = incomingChunk.Data;
            naturalisation.Type = incomingChunk.Type;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        naturalisation.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "DSCR":
                        naturalisation.Description = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ROLE":
                        naturalisation.Role = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TYPE":
                        naturalisation.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SOUR":
                        naturalisation.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "NOTE":
                        naturalisation.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OBJE":
                        naturalisation.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "ADDR":
                        naturalisation.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "AGE":
                        naturalisation.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "AGNC":
                        naturalisation.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CAUS":
                        naturalisation.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "PHON":
                        naturalisation.DatePlace.Address.Phone.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "EMAIL":
                        naturalisation.DatePlace.Address.Email.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "_PLC":
                        naturalisation.PlaceId = chunk.Reference;
                        break;

                    default:
                        resultContainer.Errors.Add($"NaturalisationParser: Failed to handle '{parentType}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }
            return naturalisation;
        }
    }
}