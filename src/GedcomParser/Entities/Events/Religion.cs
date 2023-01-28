namespace GedcomParser.Entities.Events
{
    public class Religion : Event
    {
        public Category CategoryOfReligion { get; set; } = Category.Unknown;
        public string AdoptingParentsId { get; set; }
        public enum Category
        {
            Unknown,
            Baptism,
            Christening,
        }
    }
}
