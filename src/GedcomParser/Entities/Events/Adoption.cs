namespace GedcomParser.Entities.Events
{
    public class Adoption : Event
    {
        public string AdoptingParentsId { get; set; }
        public AdoptingParents AdoptedByWhichParent { get; set; } = AdoptingParents.Unknown;
        public enum AdoptingParents
        {
            Unknown,
            Husband,
            Wife,
            Both
        }
    }
}
