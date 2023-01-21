using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public abstract class Relation
    {
        public string FamilyId { get; set; }
        public string FamilyUid { get; set; }
        public Person From { get; set; }
        public Person To { get; set; }
        public string AutoRecordId { get; set; }
        public DatePlace LastUpdateDate { get; set; }
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<Reference> References { get; set; } = new List<Reference>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public Dictionary<string, List<DatePlace>> Events { get; set; } = new Dictionary<string, List<DatePlace>>();
    }

    public class ChildRelation : Relation
    {
        public Pedigree Pedigree { get; set; }
        public string Validity { get; set; }
        public string Adoption { get; set; }
    }

    public class SpouseRelation : Relation
    {
        public List<DatePlace> Engagement           { get; set; } = new List<DatePlace>();
        public List<DatePlace> Marriage             { get; set; } = new List<DatePlace>();
        public List<DatePlace> MarriageContract     { get; set; } = new List<DatePlace>();
        public List<DatePlace> MarriageSettlement   { get; set; } = new List<DatePlace>();
        public List<DatePlace> Divorce              { get; set; } = new List<DatePlace>();
        public List<DatePlace> DivorceFiled         { get; set; } = new List<DatePlace>();
        public List<DatePlace> Annulment            { get; set; } = new List<DatePlace>();
        public List<DatePlace> MarriageBann         { get; set; } = new List<DatePlace>();
        public List<DatePlace> MarriageLicense      { get; set; } = new List<DatePlace>();
        public List<DatePlace> Separation           { get; set; } = new List<DatePlace>();
        public string Relation { get; set; }
    }

    public class SiblingRelation : Relation
    {
    }
}