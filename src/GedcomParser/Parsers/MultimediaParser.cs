using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;

namespace GedcomParser.Parsers
{
    public static class MultimediaParser
    {
        internal static Multimedia ParseMultimedia(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var multimedia = new Multimedia { Id = indiChunk.Id };

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "FILE":
                        multimedia.Files.Add(resultContainer.ParseText(chunk.Data, chunk).TrimEnd(new Char[] { '\r', '\n' }));
                        break;

                    case "TITL":
                        multimedia.Title = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "FORM":
                        multimedia.Format = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    // Genney Digit proprietary tags
                    case "_PRIM": // portrait/primary image (Y/N)
                    case "_PRNT": // if used in printing (Y/N)
                    case "_SIZE": // print size
                        // Do nothing
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Multimedia Type='{chunk.Type}'");
                        break;
                }
            }

            return multimedia;
        }
    }
}