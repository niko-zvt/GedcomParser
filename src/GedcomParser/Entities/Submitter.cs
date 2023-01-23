using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Submitter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; } = new Address();
    }
}
