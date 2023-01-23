using System.Collections.Generic;


namespace GedcomParser.Entities
{
    public class Result
    {
        public List<Person> Persons { get; set; }
        public List<ChildRelation> ChildRelations { get; set; }
        public List<SpouseRelation> SpouseRelations { get; set; }
        public List<SiblingRelation> SiblingRelations { get; set; }
        public List<Source> Sources { get; set; }
        public List<Place> Places { get; set; }
        public List<Group> Groups { get; set; }
        public List<Note> Notes { get; set; }
        public List<Submitter> Submitters { get; set; }
        public HashSet<string> LogInfo { get; set; }
        public HashSet<string> Warnings { get; set; }
        public HashSet<string> Errors { get; set; }

        public Result()
        {
            Persons = new List<Person>();
            ChildRelations = new List<ChildRelation>();
            SpouseRelations = new List<SpouseRelation>();
            SiblingRelations = new List<SiblingRelation>();
            Sources = new List<Source>();
            Places = new List<Place>();
            Groups = new List<Group>();
            Notes = new List<Note>();
            Submitters = new List<Submitter>();
            LogInfo = new HashSet<string>();
            Warnings = new HashSet<string>();
            Errors = new HashSet<string>();
        }
    }
}