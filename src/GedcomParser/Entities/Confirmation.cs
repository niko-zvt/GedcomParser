using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Confirmation
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public DatePlace DatePlace { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
    }
}
