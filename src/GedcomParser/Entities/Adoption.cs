﻿namespace GedcomParser.Entities
{
    public class Adoption
    {
        public DatePlace DatePlace { get; set; }
        public string Type { get; set; }
        public string AdoptingParents { get; set; }
        public Note Note { get; set; }
    }
}
