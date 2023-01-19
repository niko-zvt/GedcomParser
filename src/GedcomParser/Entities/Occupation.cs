using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Occupation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string PlaceId { get; set; }
        public DatePlace DatePlace { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
        public List<Citation> Citations { get; set; } = new List<Citation>();
    }
}
