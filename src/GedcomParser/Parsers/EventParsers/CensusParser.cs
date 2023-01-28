using System;
using System.Linq;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers.EventParsers
{
    public static class CensusParser
    {
        internal static Census ParseCensus(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var census = new Census();
            census.Id = incomingChunk.Id;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "WIFE":
                    case "HUSB":
                        {
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    case "AGE":
                                        {
                                            if (chunk.Type == "HUSB")
                                                census.HusbandAgeAtEvent = resultContainer.ParseText(subChunk.Data, subChunk);
                                            if (chunk.Type == "WIFE")
                                                census.WifeAgeAtEvent = resultContainer.ParseText(subChunk.Data, subChunk);
                                        }
                                        break;

                                    default:
                                        resultContainer.Errors.Add($"CensusParser: Failed to handle '{incomingChunk.Type}' -> '{chunk.Type}' -> '{subChunk.Type}'.");
                                        break;
                                }
                            }
                        }
                        break;

                    case "PLAC":
                    case "DATE":
                        census.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        census.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        census.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        census.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"CensusParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            return census;
        }
    }
}
