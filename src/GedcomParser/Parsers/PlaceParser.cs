using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;

namespace GedcomParser.Parsers
{
    public static class PlaceParser
    {
        internal static void ParsePlace(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var place = new Place { Id = indiChunk.Id };

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NAME":
                        place.Name = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ABBR":
                        place.Abbreviation = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "RIN":
                        place.AutoRecordId = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        place.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "TYPE":
                        place.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "FORM":
                        place.Form = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "_PLP":
                        // Do nothing
                        // Answer from a Genney Digit representative:
                        // "The _PLP tag is a reference to the administrative parent place id.
                        // Genney is using a place database with places organized in
                        // administrative structure and the tag is primarily for
                        // internal use in Genney."
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Place (_PLC) Type='{chunk.Type}'");
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