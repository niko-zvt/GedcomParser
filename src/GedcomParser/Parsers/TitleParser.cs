using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class TitleParser
    {
        internal static Title ParseTitle(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var title = new Title();
            title.Id = incomingChunk.Id;
            title.Text = incomingChunk.Data;
            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "CONC":
                        title.Text = title.Text + " " + chunk.Data;
                        break;

                    case "CONT":
                        title.Text = title.Text + "\n" + chunk.Data;
                        break;

                    case "PLAC":
                    case "DATE":
                        title.DatePlace = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "SOUR":
                        title.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "TYPE":
                        title.Type = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        title.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Title Type='{chunk.Type}'");
                        break;
                }
            }

            return title;
        }
    }
}
