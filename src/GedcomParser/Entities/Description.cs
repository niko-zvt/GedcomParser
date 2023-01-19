using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Description
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string PlaceId { get; set; }
        public DatePlace DatePlace { get; set; }
        public List<Citation> Citations { get; set; } = new List<Citation>();
    }
}
