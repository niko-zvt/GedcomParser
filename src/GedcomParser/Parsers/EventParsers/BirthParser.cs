using System;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class BirthParser
    {
        internal static Birth ParseBirth(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var birth = new Birth();
            birth.Id = incomingChunk.Id;
            birth.Name = incomingChunk.Data;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NOTE":
                        birth.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "TYPE":
                        birth.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ADDR":
                        birth.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "_PLC":
                        birth.PlaceId = chunk.Reference;
                        break;

                    case "SOUR":
                        birth.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "OBJE":
                        birth.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "AGNC":
                        birth.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "PLAC":
                    case "DATE":
                        birth.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "FAMC":
                        {
                            birth.ParentsId = chunk.Reference;
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    default:
                                        resultContainer.Errors.Add($"BirthParser: Failed to handle '{incomingChunk.Type}' -> '{chunk.Type}' -> '{subChunk.Type}'.");
                                        break;
                                }
                            }
                        }
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"BirthParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            return birth;
        }
    }
}