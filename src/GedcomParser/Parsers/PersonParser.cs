using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;
using static GedcomParser.Entities.Person;

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
                        person.Adoption = resultContainer.ParseAdoption(chunk);
                        break;

                    case "BAPM":
                        person.Baptized = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "BIRT":
                        person.Birth = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "BURI":
                        person.Buried = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "CHAN":
                        person.Changed = chunk.ParseDateTime();
                        break;

                    case "CHR":
                        person.Baptized = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "DEAT":
                        person.Death = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "DSCR":
                        person.Description = resultContainer.ParseDescription(chunk);
                        break;

                    case "EDUC":
                        person.Education = resultContainer.ParseEducation(chunk);
                        break;

                    case "EVEN":
                        string eventType = GetEventType(chunk);
                        if (person.Events.ContainsKey(eventType))
                        {
                            person.Events[eventType].Add(resultContainer.ParseDatePlace(chunk)); // TODO: Change parser to EVENT
                        }
                        else
                        {
                            person.Events.Add(eventType, new List<DatePlace>
                            {
                                resultContainer.ParseDatePlace(chunk) // TODO: Change parser to EVENT
                            });
                        }
                        break;

                    case "EMIG":
                        person.Emigrated.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "FACT":
                        person.Facts.Add(resultContainer.ParseFact(chunk));
                        break;

                    case "GRAD":
                        person.Graduation = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "HEAL":
                        person.Health = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "IDNO":
                        person.IdNumber = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "IMMI":
                        person.Immigrated.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "NAME":
                        var nameSections = chunk.Data.Split("/".ToCharArray(), StringSplitOptions.RemoveEmptyEntries);
                        if (nameSections.Length > 0)
                        {
                            person.FirstName = nameSections[0];
                        }

                        if (nameSections.Length > 1)
                        {
                            person.LastName = nameSections[1];
                        }

                        break;

                    case "NATI":
                        person.Nationality = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NATU":
                        person.BecomingCitizen.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "NOTE":
                        person.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "OCCU":
                        person.Occupation = resultContainer.ParseOccupation(chunk);
                        break;

                    case "RESI":
                        person.Residence.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "CENS":
                        person.Census.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "_DEST":
                        person.Destination.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "RELI":
                        person.Religion = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "SEX":
                        person.Gender = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "TITL":
                        person.Title = resultContainer.ParseText(chunk.Data, chunk);
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
                        person.NumberOfChildren = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "RIN":
                        person.AutoRecordId = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "_UPD":
                        person.LastUpdateDate = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "OBJE":
                        person.Multimedias.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "_PLC":
                        person.PlaceIds.Add(chunk.Reference);
                        break;

                    case "NMR":
                        person.NumberOfRelationships = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CONF":
                        person.Confirmation = resultContainer.ParseConfirmation(chunk);
                        break;

                    // Deliberately skipped for now
                    case "_GRP":
                        person.GroupId = chunk.Reference;
                        break;

                    default:
                        //resultContainer.Warnings.Add($"Skipped Person Type='{chunk.Type}'");
                        resultContainer.Errors.Add($"Failed to handle Person Type='{chunk.Type}'");
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