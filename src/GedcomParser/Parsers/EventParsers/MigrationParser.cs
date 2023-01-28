using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;
using System.Reflection.Metadata;
using System;
using GedcomParser.Entities;

namespace GedcomParser.Parsers.EventParsers
{
    public static class MigrationParser
    {
        internal static Migration ParseMigration(this ResultContainer resultContainer, GedcomChunk incomingChunk, Migration.Category migrationCategory = Migration.Category.Unknown)
        {
            var migration = new Migration();
            migration.Id = incomingChunk.Id;
            migration.Name = incomingChunk.Data;
            migration.CategoryOfMigration = migrationCategory;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "PLAC":
                    case "DATE":
                        migration.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        migration.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        migration.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        migration.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OBJE":
                        migration.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "ADDR":
                        migration.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "AGE":
                        migration.Age = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "AGNC":
                        migration.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CAUS":
                        migration.CauseOfEvent = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "_PLC":
                        migration.PlaceId = chunk.Reference;
                        break;

                    default:
                        var parent = incomingChunk.ParentChunk != null ? incomingChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"MigrationParser: Failed to handle '{parent}' -> '{incomingChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            return migration;
        }
    }
}
