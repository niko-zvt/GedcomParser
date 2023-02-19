using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;
using GedcomParser.Parsers.EventParsers;

namespace GedcomParser.Parsers
{
    public static class PersonParser
    {
        internal static void ParsePerson(this ResultContainer resultContainer, GedcomChunk indiChunk)
        {
            var person = new Person { Id = indiChunk.Id };

            foreach (var chunk in indiChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "_UID":
                        person.Uid = chunk.Data;
                        break;

                    case "ADOP":
                        person.Events.Add(resultContainer.ParseAdoption(chunk));
                        break;

                    case "BAPM":
                        person.Events.Add(resultContainer.ParseReligion(chunk, Religion.Category.Baptism));
                        break;

                    case "BIRT":
                        person.Events.Add(resultContainer.ParseBirth(chunk));
                        break;

                    case "CREM":
                        person.Events.Add(resultContainer.ParseFinalDisposition(chunk, FinalDisposition.Category.Cremation));
                        break;

                    case "BURI":
                        person.Events.Add(resultContainer.ParseFinalDisposition(chunk, FinalDisposition.Category.Burial));
                        break;

                    case "CHAN":
                        person.Changed = chunk.ParseDateTime();
                        break;

                    case "CHR":
                        person.Events.Add(resultContainer.ParseReligion(chunk, Religion.Category.Christening));
                        break;

                    case "DEAT":
                        person.Events.Add(resultContainer.ParseDeath(chunk));
                        break;

                    case "DSCR":
                        person.Description = resultContainer.ParseDescription(chunk);
                        break;

                    case "EDUC":
                        person.Events.Add(resultContainer.ParseEducation(chunk));
                        break;

                    case "EVEN":
                        person.Events.Add(resultContainer.ParseEvent(chunk));
                        break;

                    case "EMIG":
                        person.Events.Add(resultContainer.ParseMigration(chunk, Migration.Category.Emigration));
                        break;

                    case "FACT":
                        person.Facts.Add(resultContainer.ParseFact(chunk));
                        break;

                    case "GRAD":
                        person.Events.Add(resultContainer.ParseEvent(chunk));
                        break;

                    case "HEAL":
                        person.Health = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "IDNO":
                        person.IdNumbers.Add(resultContainer.ParseIdentifier(chunk));
                        break;

                    case "IMMI":
                        person.Events.Add(resultContainer.ParseMigration(chunk, Migration.Category.Immigration));
                        break;

                    case "NAME":
                        {
                            var nameSections = chunk.Data.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                            if (nameSections.Length > 0)
                            {
                                person.FirstName = nameSections[0];
                            }

                            if (nameSections.Length > 1)
                            {
                                person.LastName = nameSections[1];
                            }
                        }
                        break;

                    case "NATI":
                        person.Events.Add(resultContainer.ParseNationality(chunk));
                        break;

                    case "NATU":
                        person.Events.Add(resultContainer.ParseNaturalisation(chunk));
                        break;

                    case "NOTE":
                        person.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OCCU":
                        person.Events.Add(resultContainer.ParseOccupation(chunk));
                        break;

                    case "RESI":
                        person.Events.Add(resultContainer.ParseResidence(chunk));
                        break;

                    case "CENS":
                        person.Events.Add(resultContainer.ParseCensus(chunk));
                        break;

                    case "_DEST":
                        person.Events.Add(resultContainer.ParseEvent(chunk));
                        break;

                    case "RELI":
                        person.Events.Add(resultContainer.ParseReligion(chunk));
                        break;

                    case "SEX":
                        person.Gender = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TITL":
                        person.Title = resultContainer.ParseTitle(chunk);
                        break;

                    case "SOUR":
                        person.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "FAMS":
                        {
                            person.FamilyId = chunk.Reference;
                            foreach(var subChunk in chunk.SubChunks)
                            {
                                switch(subChunk.Type)
                                {
                                    case "NOTE":
                                        person.Notes.Add(resultContainer.ParseNote(subChunk.Data, subChunk));
                                        break;

                                    default:
                                        resultContainer.Warnings.Add($"Skipped '{subChunk.Type}' in Person Type='{chunk.Type}'");
                                        break;
                                }
                            }
                        }
                        break;

                    case "FAMC":
                        {
                            person.FamilyChildId = chunk.Reference;
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    case "PEDI":
                                        person.Pedigree = resultContainer.ParsePedigree(subChunk);
                                        break;

                                    case "NOTE":
                                        person.Notes.Add(resultContainer.ParseNote(subChunk.Data, subChunk));
                                        break;

                                    default:
                                        resultContainer.Warnings.Add($"Skipped '{subChunk.Type}' in Person Type='{chunk.Type}'");
                                        break;
                                }
                            }
                        }
                        break;

                    case "HIST":
                        {
                            person.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        }
                        break;

                    case "NCHI":
                        {
                            person.NumberOfChildren = chunk.Data;
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    case "TYPE":
                                        // Do nothing.
                                        break;

                                    case "NOTE":
                                        person.Notes.Add(resultContainer.ParseNote($"Number of children: {chunk.Data}." + " " + subChunk.Data, subChunk));
                                        break;

                                    case "DATE":
                                    case "PLAC":
                                        person.Events.Add(resultContainer.ParseEvent(chunk));
                                        break;

                                    case "SOUR":
                                        {
                                            var citation = resultContainer.ParseCitation(chunk);
                                            citation.Text = $"Number of children: {chunk.Data}." + " " + citation.Text;
                                            person.Citations.Add(citation);
                                        }
                                        break;

                                    default:
                                        resultContainer.Warnings.Add($"Skipped '{subChunk.Type}' in Person Type='{chunk.Type}'");
                                        break;
                                }
                            }
                        }
                        break;

                    case "RIN":
                        person.AutoRecordId = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "_UPD":
                        person.LastUpdateDate = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "OBJE":
                        person.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "_PLC":
                        person.PlaceIds.Add(chunk.Reference);
                        break;

                    case "NMR":
                        {
                            person.NumberOfRelationships = chunk.Data;
                            foreach (var subChunk in chunk.SubChunks)
                            {
                                switch (subChunk.Type)
                                {
                                    case "TYPE":
                                        // Do nothing.
                                        break;

                                    case "NOTE":
                                        person.Notes.Add(resultContainer.ParseNote($"Number of relationships: {chunk.Data}." + " " + subChunk.Data, subChunk));
                                        break;

                                    case "DATE":
                                    case "PLAC":
                                        person.Events.Add(resultContainer.ParseEvent(chunk));
                                        break;

                                    case "SOUR":
                                        {
                                            var citation = resultContainer.ParseCitation(chunk);
                                            citation.Text = $"Number of relationships: {chunk.Data}." + " " + citation.Text;
                                            person.Citations.Add(citation);
                                        }
                                        break;

                                    default:
                                        resultContainer.Warnings.Add($"Skipped '{subChunk.Type}' in Person Type='{chunk.Type}'");
                                        break;
                                }
                            }
                        }
                        break;

                    case "CONF":
                        person.Events.Add(resultContainer.ParseConfirmation(chunk));
                        break;

                    case "FCOM":
                        person.Events.Add(resultContainer.ParseFirstCommunion(chunk));
                        break;

                    case "_GRP":
                        person.GroupId = chunk.Reference;
                        break;

                    default:
                        var parent = indiChunk.ParentChunk != null ? indiChunk.ParentChunk.Type : " -- ";
                        resultContainer.Errors.Add($"PersonParser: Failed to handle '{parent}' -> '{indiChunk.Type}' -> '{chunk.Type}'.");
                        break;
                }
            }

            resultContainer.Persons.Add(person);
        }
        internal static string GetEventType(GedcomChunk chunk)
        {
            return chunk.SubChunks.SingleOrDefault(c => c.Type == "TYPE")?.Data.ToLower();
        }
    }
}