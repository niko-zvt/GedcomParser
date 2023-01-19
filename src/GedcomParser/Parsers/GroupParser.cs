using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;

namespace GedcomParser.Parsers
{
    public static class GroupParser
    {
        internal static void ParseGroup(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var group = new Entities.Group { Id = indiChunk.Id };

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NAME":
                        group.Name = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        group.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Group (_GRP) Type='{chunk.Type}'");
                        break;
                }
            }
            resultContainer.Groups.Add(group);
        }
    }
}