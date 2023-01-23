using System.Collections.Generic;


namespace GedcomParser.Entities.Internal
{
    /// <summary>
    /// This represents a line from the GEDCOM file AND all its related sub lines in a structured hierarchy.
    /// </summary>
    internal class GedcomChunk : GedcomLine
    {
        internal GedcomChunk ParentChunk { get; set; }
        internal List<GedcomChunk> SubChunks { get; }

        internal GedcomChunk(GedcomLine gedcomLine) : base(gedcomLine.Level, gedcomLine.Id, gedcomLine.Type, gedcomLine.Data, gedcomLine.Reference)
        {
            ParentChunk = null;
            SubChunks = new List<GedcomChunk>();
        }
    }
}