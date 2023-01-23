using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class ChunkParser
    {
        internal static ResultContainer ParseChunks(this IEnumerable<GedcomChunk> chunks)
        {
            var resultContainer = new ResultContainer();

            foreach (var chunk in chunks.OrderBy(Priority))
            {
                switch (chunk.Type)
                {
                    case "HEAD":
                    case "TRLR":
                        // Do nothing.
                        break;

                    case "SUBN":
                        // Do nothing.
                        // Specific to Ancestral File; obsolete.
                        break;

                    case "FAM":
                        resultContainer.ParseFamily(chunk);
                        break;

                    case "INDI":
                        resultContainer.ParsePerson(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "SOUR":
                        resultContainer.ParseSource(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "_PLC":
                        resultContainer.ParsePlace(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "_GRP":
                        resultContainer.ParseGroup(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "NOTE":
                        // resultContainer.ParseNote(chunk); // TODO
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "SUBM":
                        resultContainer.ParseSubmitter(chunk);
                        resultContainer.AddIdChunk(chunk);
                        break;

                    case "OBJE":
                    case "REPO":
                    case "CSTA": // Child status; used as 'enum' by Reunion software
                        //resultContainer.Warnings.Add($"Failed to handle top level Type='{chunk.Type}'");
                        break;
 
                    case "GEDC":
                    default:
                        resultContainer.Errors.Add($"Failed to handle top level Type='{chunk.Type}'");
                        break;
                }
            }

            return resultContainer;
        }

        private static int Priority(GedcomChunk chunk)
        {
            switch (chunk.Type)
            {
                case "NOTE":
                case "_PLC":
                    return 0;
                case "SOUR":
                    return 1;
                case "INDI":
                    return 2;
                case "FAM":
                    return 3;
                default:
                    return 0;
            }
        }
    }
}