﻿using System.Collections.Generic;

namespace GedcomParser.Entities
{
    public class DatePlace
    {
        public string Date { get; set; }
        public string PlaceId { get; set; }
        public string Place { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }
        public List<Note> Notes { get; set; } = new List<Note>();
        public string ResponsibleAgency { get; set; }
        public string HusbandAgeAtEvent { get; set; }
        public string WifeAgeAtEvent { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }

        // The age of the individual at the time an event occurred, or the age listed in the document.
        public string Age { get; set; }
        public List<Multimedia> Multimedia { get; set; } = new List<Multimedia>();
        public Address Address { get; set; } = new Address();
        public string CauseOfEvent { get; set; }
        public Citation Citation { get; set; } = new Citation();
    }
}