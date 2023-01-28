using GedcomParser.Entities.Events;
using GedcomParser.Entities.Internal;
using System.Reflection.Metadata;
using System;
using GedcomParser.Entities;
using System.Collections.Generic;

namespace GedcomParser.Parsers.EventParsers
{
    public static class FinalDispositionParser
    {
        internal static FinalDisposition ParseFinalDisposition(this ResultContainer resultContainer, GedcomChunk incomingChunk, FinalDisposition.Category dispositionType = FinalDisposition.Category.Unknown)
        {
            var currentEvent = resultContainer.ParseEvent(incomingChunk);
            var disposition = new FinalDisposition();
            disposition.Id = currentEvent.Id;
            disposition.Name = currentEvent.Name;
            disposition.Role = currentEvent.Role;
            disposition.Type = currentEvent.Type;
            disposition.Age = currentEvent.Age;
            disposition.ResponsibleAgency = currentEvent.ResponsibleAgency;
            disposition.PlaceId = currentEvent.PlaceId;
            disposition.CauseOfEvent = currentEvent.CauseOfEvent;
            disposition.Address = currentEvent.Address;
            disposition.DatePlace = currentEvent.DatePlace;
            disposition.Description = currentEvent.Description;
            disposition.Notes = currentEvent.Notes;
            disposition.Citations = currentEvent.Citations;
            disposition.Multimedia = currentEvent.Multimedia;
            disposition.CategoryOfFinalDisposition = dispositionType;
            return disposition;
        }
    }
}
