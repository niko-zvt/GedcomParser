using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Data
    {
        public string Id { get; set; }
        public string ResponsibleAgency { get; set; }
        public List<Event> Events { get; set; } = new List<Event>();
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}