using System.Collections.Generic;
using GedcomParser.Entities.Events;

namespace GedcomParser.Entities
{
    public class Person
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string FamilyId { get; set; }
        public string FamilyChildId { get; set; }
        public Pedigree Pedigree { get; set; }
        public string NumberOfChildren { get; set; }
        public string NumberOfRelationships { get; set; }
        public string Changed { get; set; }
        public string Health { get; set; }
        public Title Title { get; set; }
        public Description Description { get; set; }
        public string GroupId { get; set; }
        public string AutoRecordId { get; set; }
        public Address Address { get; set; }
        public DatePlace LastUpdateDate { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Fact> Facts { get; set; } = new List<Fact>();
        public List<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<string> PlaceIds { get; set; } = new List<string>();
        public List<Identifier> IdNumbers { get; set; } = new List<Identifier>();
    }
}