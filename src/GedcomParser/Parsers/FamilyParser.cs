using System;
using System.Collections.Generic;
using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Parsers.EventParsers;

namespace GedcomParser.Parsers
{
    public static class FamilyParser
    {
        internal static void ParseFamily(this ResultContainer resultContainer, GedcomChunk famChunk)
        {
            var spousalRelation = new SpouseRelation();
            string relation = null;
            string uid = null;
            var parents = new List<Person>();
            var children = new List<Person>();

            foreach (var chunk in famChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "_UID":
                        uid = chunk.Data;
                        break;

                    case "CHIL":
                        var child = resultContainer.Persons.SingleOrDefault(p => p.Id == chunk.Reference);
                        if (child != null)
                        {
                            children.Add(child);
                        }
                        break;

                    case "DIV":
                        spousalRelation.Divorce.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "DIVF":
                        spousalRelation.DivorceFiled.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "ANUL":
                        spousalRelation.Annulment.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "HUSB":
                        var husband = resultContainer.Persons.SingleOrDefault(p => p.Id == chunk.Reference);
                        if (husband != null)
                        {
                            parents.Add(husband);
                        }
                        break;

                    case "_REL":
                        relation = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "MARR": // TODO: Change parser
                        spousalRelation.Marriage.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "MARC":
                        spousalRelation.MarriageContract.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "MARB":
                        spousalRelation.MarriageBann.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "MARL":
                        spousalRelation.MarriageLicense.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "_SEPR":
                        spousalRelation.Separation.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "MARS":
                        spousalRelation.MarriageSettlement.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "ENGA":
                        spousalRelation.Engagement.Add(resultContainer.ParseDatePlace(chunk));
                        break;

                    case "NOTE":
                        spousalRelation.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    case "WIFE":
                        var wife = resultContainer.Persons.SingleOrDefault(p => p.Id == chunk.Reference);
                        if (wife != null)
                        {
                            parents.Add(wife);
                        }
                        break;

                    case "RIN":
                        spousalRelation.AutoRecordId = chunk.Data;
                        break;

                    case "CHAN":
                    case "_UPD": // TODO: If LastUpdateDate != null we must select newest date
                        if(spousalRelation.LastUpdateDate != null && spousalRelation.LastUpdateDate.Date != null)
                            resultContainer.Warnings.Add($"Warning! In Family Type='{chunk.Type}' field LastUpdateDate is not null.");
                        spousalRelation.LastUpdateDate = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        spousalRelation.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "EVEN":
                        string eventType = PersonParser.GetEventType(chunk);
                        if (spousalRelation.Events.ContainsKey(eventType))
                        {
                            spousalRelation.Events[eventType].Add(resultContainer.ParseDatePlace(chunk)); // TODO: Change parser to EVENT
                        }
                        else
                        {
                            spousalRelation.Events.Add(eventType, new List<DatePlace>
                            {
                                resultContainer.ParseDatePlace(chunk) // TODO: Change parser to EVENT
                            });
                        }
                        break;

                    case "REFN":
                        spousalRelation.References.Add(resultContainer.ParseReference(chunk));
                        break;

                    // Deliberately skipped for now
                    case "CENS":
                        spousalRelation.Censuses.Add(resultContainer.ParseCensus(chunk));
                        break;

                    case "SLGS":
                        break;

                    case "SUBM":
                        break;

                    case "DSCR":
                    case "FAMS":
                    case "FAMC":
                    case "HIST":
                    case "NCHI":
                    case "NMR":
                    case "OBJE":
                    case "PAGE":
                        resultContainer.Warnings.Add($"Skipped Family Type='{chunk.Type}'");
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Family Type='{chunk.Type}'");
                        break;
                }
            }

            // Spouses
            if (parents.Count == 2)
            {
                resultContainer.SpouseRelations.Add(new SpouseRelation
                {
                    FamilyId = famChunk.Id,
                    FamilyUid = uid,
                    From = parents[0],
                    To = parents[1],
                    Engagement          = spousalRelation.Engagement,
                    Marriage            = spousalRelation.Marriage,
                    MarriageContract    = spousalRelation.MarriageContract,
                    MarriageSettlement  = spousalRelation.MarriageSettlement,
                    DivorceFiled        = spousalRelation.DivorceFiled,
                    Divorce             = spousalRelation.Divorce,
                    Annulment           = spousalRelation.Annulment,
                    MarriageBann        = spousalRelation.MarriageBann,
                    MarriageLicense     = spousalRelation.MarriageLicense,
                    Separation          = spousalRelation.Separation,
                    Relation            = relation,
                    Notes               = spousalRelation.Notes,
                });
                resultContainer.SpouseRelations.Add(new SpouseRelation
                {   
                    FamilyId = famChunk.Id,
                    FamilyUid = uid,
                    From = parents[1],
                    To = parents[0],
                    Engagement          = spousalRelation.Engagement,
                    Marriage            = spousalRelation.Marriage,
                    MarriageContract    = spousalRelation.MarriageContract,
                    MarriageSettlement  = spousalRelation.MarriageSettlement,
                    DivorceFiled        = spousalRelation.DivorceFiled,
                    Divorce             = spousalRelation.Divorce,
                    Annulment           = spousalRelation.Annulment,
                    MarriageBann        = spousalRelation.MarriageBann,
                    MarriageLicense     = spousalRelation.MarriageLicense,
                    Separation          = spousalRelation.Separation,
                    Relation            = relation,
                    Notes               = spousalRelation.Notes,
                });
            }

            // Parents / Children
            foreach (var parent in parents)
            {
                foreach (var child in children)
                {
                    var childRelation = new ChildRelation { FamilyId = famChunk.Id, From = child, To = parent };
                    AddStatus(resultContainer, childRelation);
                    resultContainer.ChildRelations.Add(childRelation);
                }
            }

            // Siblings
            foreach (var child1 in children)
            {
                foreach (var child2 in children.Where(c => c.Id != child1.Id))
                {
                    resultContainer.SiblingRelations.Add(new SiblingRelation { FamilyId = famChunk.Id, From = child1, To = child2 });
                }
            }
        }

        /// <summary>
        /// Lookup possible information on child legal status.
        /// It is stored in separate chunks outside the Individual and Family chunks.
        /// </summary>
        private static void AddStatus(this ResultContainer resultContainer, ChildRelation childRelation)
        {
            var childChunk = resultContainer.GetIdChunk(childRelation.From.Id);
            if (childChunk != null)
            {
                foreach (var chunk1 in childChunk.SubChunks)
                {
                    switch (chunk1.Type)
                    {
                        case "FAMC":
                            foreach (var chunk2 in chunk1.SubChunks)
                            {
                                switch (chunk2.Type)
                                {
                                    case "PEDI":
                                        childRelation.Pedigree = resultContainer.ParsePedigree(chunk2);
                                        break;

                                    case "STAT":
                                        childRelation.Validity = chunk2.Data;
                                        break;

                                    case "ADOP":
                                        var adoptionInfo = new List<string>();
                                        foreach (var chunk3 in chunk1.SubChunks)
                                        {
                                            switch (chunk3.Type)
                                            {
                                                case "DATE":
                                                    adoptionInfo.Add(chunk3.ParseDateTime());
                                                    break;
                                                case "STAT":
                                                case "NOTE":
                                                    adoptionInfo.Add(chunk3.Data);
                                                    break;
                                            }
                                        }
                                        childRelation.Adoption = string.Join(", ", adoptionInfo);
                                        break;

                                    case "NOTE":
                                        childRelation.Notes.Add(resultContainer.ParseNote(chunk2.Data, chunk2));
                                        break;

                                    default:
                                        resultContainer.Errors.Add($"Failed to handle Status Type='{chunk2.Type}'");
                                        break;
                                }
                            }

                            break;
                    }
                }
            }
        }
    }
}
