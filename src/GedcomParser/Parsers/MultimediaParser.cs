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
                    default:
                        resultContainer.Errors.Add($"Failed to handle Multimedia Type='{chunk.Type}'");
                        break;
                }
            }

            return multimedia;
        }
    }
}