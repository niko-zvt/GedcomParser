using System;
using System.Collections.Generic;
using System.Text;

namespace GedcomParser.Entities
{
    public class Pedigree
    {
        public PedigreeType Type { get; set; }
        public enum PedigreeType
        {
            Adopted, // Indicates adoptive parents.
            Birth,   // Indicates official parents (birth parents).
            Foster,  // Indicates child was included in a foster or guardian family.
        }
    }
}
