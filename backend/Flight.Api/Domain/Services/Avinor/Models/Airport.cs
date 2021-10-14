﻿using System.Xml.Serialization;

namespace Flight.Api.Domain.Services.Avinor.Models
{
    public class Airport
    {
        [XmlAttribute("code")]
        public string Code { get; set; }
        [XmlAttribute("name")]
        public string Name { get; set; }
    }
}