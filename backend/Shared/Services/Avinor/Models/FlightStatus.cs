﻿using System.Xml.Serialization;

namespace Shared.Services.Avinor.Models
{
    public class FlightStatus
    {
        [XmlAttribute("code")]
        public string Code { get; set; }
        [XmlAttribute("statusTextEn")]
        public string TextEnglish { get; set; }
        [XmlAttribute("statusTextNo")]
        public string TextNorwegian { get; set; }
    }
}