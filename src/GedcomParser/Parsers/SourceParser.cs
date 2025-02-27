﻿using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;

namespace GedcomParser.Parsers
{
    public static class SourceParser
    {
        internal static void ParseSource(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var source = new Source { Id = indiChunk.Id };

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    /*
                     n <XREF:SOUR> SOUR
                       +1 DATA
                            +2 EVEN <EVENTS_RECORDED>
                                +3 DATE <DATE_PERIOD>
                                +3 PLAC <SOURCE_JURISDICTION_PLACE>
                            +2 AGNC <RESPONSIBLE_AGENCY>
                            +2 <<NOTE_STRUCTURE>>
                       +1 AUTH <SOURCE_ORIGINATOR>
                       +1 TITL <SOURCE_DESCRIPTIVE_TITLE>
                       +1 ABBR <SOURCE_FILED_BY_ENTRY>
                       +1 PUBL <SOURCE_PUBLICATION_FACTS>
                       +1 TEXT <TEXT_FROM_SOURCE>
                       +1 <<SOURCE_REPOSITORY_CITATION>>
                       +1 REFN <USER_REFERENCE_NUMBER>
                            +2 TYPE <USER_REFERENCE_TYPE>
                       +1 RIN <AUTOMATED_RECORD_ID>
                       +1 <<CHANGE_DATE>>
                       +1 <<NOTE_STRUCTURE>>
                       +1 <<MULTIMEDIA_LINK>>
                     */

                    case "DATA":
                        source.Data = resultContainer.ParseData(chunk);
                        break;

                    case "AUTH":
                        source.Author = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TITL":
                        source.Title = resultContainer.ParseTitle(chunk);
                        break;

                    case "ABBR":
                        source.Abbreviation = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "PUBL":
                        source.Publication = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TEXT":
                        source.Text.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "REPO":
                        source.RepositoryId = chunk.Reference; // TODO - Add repository class
                        break;

                    case "REFN":
                        source.References.Add(resultContainer.ParseReference(chunk));
                        break;

                    case "RIN":
                        source.AutoRecordId = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NAME":
                        source.Name = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "_TYPE":
                        source.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;
                    
                    case "MEDI":
                    case "_MEDI":
                        source.Media = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "OBJE":
                        source.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "NOTE":
                        source.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "CHAN":
                        source.LastUpdateDate = resultContainer.ParseDatePlace(chunk);
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Source Record Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Sources.Add(source);
        }

        internal static string GetSourceType(GedcomChunk chunk)
        {
            return chunk.SubChunks.SingleOrDefault(c => c.Type == "TYPE")?.Data.ToLower();
        }
    }
}