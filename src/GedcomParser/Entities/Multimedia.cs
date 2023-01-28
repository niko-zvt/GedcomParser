using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Multimedia
    {
        public string Id { get; set; }
        public Title Title { get; set; }
        public List<string> Files { get; set; } = new List<string>();
        public string Format { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
    }
}