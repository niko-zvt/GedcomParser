namespace GedcomParser.Entities.Events
{
    public class Migration : Event
    {
        public Category CategoryOfMigration { get; set; } = Category.Unknown;
        public enum Category
        {
            Unknown,
            Emigration,
            Immigration
        }
    }
}
