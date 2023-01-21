using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Education
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DatePlace Date { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}
