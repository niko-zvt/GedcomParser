using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Occupation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PlaceId { get; set; }
        public string Age { get; set; }
        public string ResponsibleAgency { get; set; }
        public string Type { get; set; }
        public string CauseOfEvent { get; set; }
        public Address Address { get; set; }
        public DatePlace DatePlace { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
    }
}
