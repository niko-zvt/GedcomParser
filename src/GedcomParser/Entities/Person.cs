using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Person
    {
        public string Id { get; set; }
        public string Uid { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DatePlace Birth { get; set; }
        public DatePlace Death { get; set; }
        public DatePlace Buried { get; set; }
        public DatePlace Baptized { get; set; }
        public string FamilyId { get; set; }
        public string FamilyChildId { get; set; }
        public string NumberOfChildren { get; set; }
        public string NumberOfRelationships { get; set; }
        public Pedigree Pedigree { get; set; }
        public Confirmation Confirmation { get; set; }
        public Nationality Nationality { get; set; }
        public string Changed { get; set; }
        public Occupation Occupation { get; set; }
        public string Health { get; set; }
        public string Title { get; set; }
        public Description Description { get; set; }
        public string GroupId { get; set; }
        public string AutoRecordId { get; set; }
        public Address Address { get; set; }
        public Adoption Adoption { get; set; }
        public List<DatePlace> Residence { get; set; } = new List<DatePlace>();
        public List<DatePlace> Emigrated { get; set; } = new List<DatePlace>();
        public List<DatePlace> Immigrated { get; set; } = new List<DatePlace>();
        public List<DatePlace> BecomingCitizen { get; set; } = new List<DatePlace>();
        public List<Religion> Religions { get; set; } = new List<Religion>();
        public List<Education> Educations { get; set; } = new List<Education>();
        public DatePlace Graduation { get; set; }
        public DatePlace LastUpdateDate { get; set; }
        public Dictionary<string, List<DatePlace>> Events { get; set; } = new Dictionary<string, List<DatePlace>>();
        public List<DatePlace> Census { get; set; } = new List<DatePlace>();
        public List<DatePlace> Destination { get; set; } = new List<DatePlace>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Fact> Facts { get; set; } = new List<Fact>();
        public List<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<string> PlaceIds { get; set; } = new List<string>();
        public List<Identifier> IdNumbers { get; set; } = new List<Identifier>();
    }
}