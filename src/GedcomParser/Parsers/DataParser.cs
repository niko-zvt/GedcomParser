using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class DataParser
    {
        internal static Data ParseData(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var data = new Data();
            data.Id = incomingChunk.Id;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "EVEN":
                        data.Events.Add(resultContainer.ParseEvent(chunk));
                        break;

                    case "AGNC":
                        data.ResponsibleAgency = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "NOTE":
                        data.Notes.Add(resultContainer.ParseNote(chunk.Data, chunk));
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Data Type='{chunk.Type}'");
                        break;
                }
            }

            return data;
        }
    }
}
