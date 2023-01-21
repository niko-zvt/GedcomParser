using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Note
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public string AutoRecordId { get; set; }
        public DatePlace Date { get; set; }
        public DatePlace LastUpdateDate { get; set; }
        public string PlaceId { get; set; }
        public List<Citation> Citations { get; set; } = new List<Citation>();
        public List<Reference> References { get; set; } = new List<Reference>();
        public bool IsBlobContentSkipped { get; set; } = false;
    }
}
