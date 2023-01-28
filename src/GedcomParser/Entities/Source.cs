using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Source
    {
        public string Id { get; set; }
        public string Author { get; set; }
        public string Name { get; set; }
        public Title Title { get; set; }
        public string Abbreviation { get; set; }
        public string Publication { get; set; }
        public List<string> Text { get; set; } = new List<string>();
        public List<Reference> References { get; set; } = new List<Reference>();
        public string RepositoryId { get; set; }
        public string ResponsibleAgency { get; set; }
        public string AutoRecordId { get; set; }
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<Note> Notes { get; set; } = new List<Note>();
        public Data Data { get; set; }
        public DatePlace LastUpdateDate { get; set; }
        public string Type { get; set; }
        public string Media { get; set; }
        public List<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
    }
}