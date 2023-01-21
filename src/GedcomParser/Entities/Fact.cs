using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Fact
    {
        public string Id { get; set; }
        public string Type { get; set; }
        public string Text { get; set; }
        public string PlaceId { get; set; }
        public DatePlace DatePlace { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public List<Citation> Citations { get; set; } = new List<Citation>();
    }
}
