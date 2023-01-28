using System;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class AdoptionParser
    {
        internal static Adoption ParseAdoption(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var adoption = new Adoption();
            adoption.Id = incomingChunk.Id;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NOTE":
                        adoption.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "TYPE":
                        adoption.Type = chunk.Data;
                        break;

                    case "SOUR":
                        adoption.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "PLAC":
                    case "DATE":
                        adoption.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "_PLC":
                        adoption.PlaceId = chunk.Reference;
                        break;

                    case "FAMC":
                        {
                            adoption.AdoptingParentsId = chunk.Reference;
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    case "ADOP":
                                        {
                                            if (subChunk.Data == "HUSB")
                                            {
                                                adoption.AdoptedByWhichParent = Adoption.AdoptingParents.Husband;
                                            }
                                            else if (subChunk.Data == "WIFE")
                                            {
                                                adoption.AdoptedByWhichParent = Adoption.AdoptingParents.Wife;
                                            }
                                            else if (subChunk.Data == "BOTH")
                                            {
                                                adoption.AdoptedByWhichParent = Adoption.AdoptingParents.Both;
                                            }
                                            else
                                            {
                                                resultContainer.Errors.Add($"AdoptionParser: Failed to handle '{incomingChunk.Type}' -> '{chunk.Type}' -> '{subChunk.Type}' -> Data '{subChunk.Data}'.");
                                            }
                                        }
                                        break;

                                    default:
                                        resultContainer.Errors.Add($"AdoptionParser: Failed to handle '{incomingChunk.Type}' -> '{chunk.Type}' -> '{subChunk.Type}'.");
                                        break;
                                }
                            }
                        }
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"AdoptionParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            return adoption;
        }
    }
}