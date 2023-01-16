using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;

namespace GedcomParser.Parsers
{
    public static class PlaceRecordParser
    {
        internal static void PlaceParser(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var place = new PlaceRecord { Id = indiChunk.Id };

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    // Deliberately skipped for now
                    //case "_TYPE":
                    //case "_MEDI":
                    //    resultContainer.Warnings.Add($"Skipped Source Record Type='{chunk.Type}'");
                    //    break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Place Record Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Places.Add(place);
        }

        internal static string GetPlaceType(GedcomChunk chunk)
        {
            return chunk.SubChunks.SingleOrDefault(c => c.Type == "TYPE")?.Data.ToLower();
        }
    }
}