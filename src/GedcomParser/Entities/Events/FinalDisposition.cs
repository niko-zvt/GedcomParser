namespace GedcomParser.Entities.Events
{
    public class FinalDisposition : Event
    {
        public Category CategoryOfFinalDisposition { get; set; } = Category.Unknown;
        public enum Category
        {
            Unknown,
            Burial,
            Cremation,
        }
    }
}
