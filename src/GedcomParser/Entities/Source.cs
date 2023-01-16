using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Source
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public string Title { get; set; }
        public string Abbreviation { get; set; }
        public string Publication { get; set; }
        public List<string> Text { get; set; } = new List<string>();
        public string Reference { get; set; }
        public string RepositoryId { get; set; }
        public string ResponsibleAgency { get; set; }
        public string AutoRecordId { get; set; }
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<string> Notes { get; set; } = new List<string>();
        public List<Event> Events { get; set; } = new List<Event>();
        public string Type { get; set; }
        public string Media { get; set; }
        public List<Multimedia> Multimedias { get; set; } = new List<Multimedia>();
    }
}