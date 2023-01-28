using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;
using System.Reflection.Metadata;
using System;
using GedcomParser.Entities;

namespace GedcomParser.Parsers.EventParsers
{
    public static class ReligionParser
    {
        internal static Religion ParseReligion(this ResultContainer resultContainer, GedcomChunk incomingChunk, Religion.Category religionType = Religion.Category.Unknown)
        {
            var religion = new Religion();
            religion.Id = incomingChunk.Id;
            religion.Name = incomingChunk.Data;
            religion.CategoryOfReligion = religionType;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        religion.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        religion.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        religion.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        religion.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OBJE":
                        religion.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "ADDR":
                        religion.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "AGE":
                        religion.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "AGNC":
                        religion.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CAUS":
                        religion.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "_PLC":
                        religion.PlaceId = chunk.Reference;
                        break;

                    case "FAMC":
                        {
                            religion.AdoptingParentsId = chunk.Reference;
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    default:
                                        resultContainer.Errors.Add($"ReligionParser: Failed to handle '{incomingChunk.Type}' -> '{chunk.Type}' -> '{subChunk.Type}'.");
                                        break;
                                }
                            }
                        }
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"ReligionParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            return religion;
        }
    }
}
