using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Multimedia
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public List<string> Files = new List<string>();
        public string Format { get; set; }
    }
}