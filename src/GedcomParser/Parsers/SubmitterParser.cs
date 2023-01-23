using System.Linq;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;


namespace GedcomParser.Parsers
{
    public static class SubmitterParser
    {
        internal static void ParseSubmitter(this ResultContainer resultContainer, GedcomChunk incomingChunk)
        {
            var submitter = new Submitter();
            submitter.Id = incomingChunk.Id;

            foreach (var chunk in incomingChunk.SubChunks)
            {
                switch (chunk.Type)
                {
                    case "NAME":
                        submitter.Name = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "ADDR":
                        submitter.Address = resultContainer.ParseAddress(chunk);
                        break;

                    case "PHON":
                        submitter.Address.Phone.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "EMAIL":
                        submitter.Address.Email.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "WWW":
                        submitter.Address.Web.Add(resultContainer.ParseText(chunk.Data, chunk));
                        break;

                    case "RFN": // SUBM.RFN Specific to Ancestral File; obsolete.
                        resultContainer.LogInfo.Add($"Skipped '{chunk.Type}' tag in Submitter");
                        break;

                    case "OBJE":
                        submitter.Multimedia.Add(resultContainer.ParseMultimedia(chunk));
                        break;

                    case "CHAN":
                        submitter.LastUpdateDate = resultContainer.ParseDatePlace(chunk);
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Submitter Type='{chunk.Type}'");
                        break;
                }
            }

            resultContainer.Submitters.Add(submitter);
        }
    }
}
