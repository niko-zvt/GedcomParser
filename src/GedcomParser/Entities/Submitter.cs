using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class Submitter
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Language { get; set; }
        public string AutoRecordId { get; set; }
        public Address Address { get; set; } = new Address();
        public DatePlace LastUpdateDate { get; set; } = new DatePlace();
        public List<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
    }
}
