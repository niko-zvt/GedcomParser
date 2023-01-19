using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Group
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
    }
}