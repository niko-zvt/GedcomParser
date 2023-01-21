using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Note
    {
        public string Id { get; set; }
        public string Text { get; set; }
        public string Type { get; set; }
        public bool IsBlobContentSkipped { get; set; } = false;
    }
}
