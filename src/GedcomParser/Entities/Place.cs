using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Place
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Abbreviation { get; set; }
        public string Type { get; set; }
        public List<string> Notes { get; set; } = new List<string>();
        public string AutoRecordId { get; set; }
        public string Form { get; set; }
    }
}