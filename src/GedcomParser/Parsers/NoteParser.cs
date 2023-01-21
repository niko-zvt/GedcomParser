using System;
using System.Text;
using GedcomParser.Entities;
using GedcomParser.Entities.Internal;
using GedcomParser.Extensions;


namespace GedcomParser.Parsers
{
    public static class NoteParser
    {
        internal static Note ParseNote(this ResultContainer resultContainer, string previousNote, GedcomChunk incomingChunk)
        {
            var note = new Note();

            var noteChunk = incomingChunk;
            if (incomingChunk.Reference.IsSpecified())
            {
                noteChunk = resultContainer.GetIdChunk(noteChunk.Reference);
                if (noteChunk == null)
                {
                    throw new Exception($"Unable to find Note with Id='{incomingChunk.Reference}'");
                }
            }

            note.Type = noteChunk.Type;

            foreach (var chunk in noteChunk.SubChunks)
            {
                if (chunk.IsUnwantedBlob())
                {
                    note.IsBlobContentSkipped = true;
                    break;
                }

                switch (chunk.Type)
                {
                    case "CONC":
                        note.Text = note.Text + " " + resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CONT":
                        note.Text = note.Text + "\n" + resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    case "CHAN":
                        note.LastUpdateDate = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "PLAC":
                    case "DATE":
                        note.Date = resultContainer.ParseDatePlace(chunk);
                        break;

                    case "_PLC":
                        note.PlaceId = chunk.Reference;
                        break;

                    case "SOUR":
                        note.Citations.Add(resultContainer.ParseCitation(chunk));
                        break;

                    case "REFN":
                        note.References.Add(resultContainer.ParseReference(chunk));
                        break;

                    case "RIN":
                        note.AutoRecordId = resultContainer.ParseText(chunk.Data, chunk);
                        break;

                    default:
                        resultContainer.Errors.Add($"Failed to handle Note Type='{chunk.Type}'");
                        break;
                }
            }

            if (previousNote.IsSpecified())
            {
                note.Text = previousNote + Environment.NewLine + note.Text;
            }

            return note;
        }

        private static bool IsUnwantedBlob(this GedcomChunk chunk)
        {
            // TODO: We should probably make this check more intelligent 
            return chunk.Data?.Contains("<span") ?? false;
        }
    }
}