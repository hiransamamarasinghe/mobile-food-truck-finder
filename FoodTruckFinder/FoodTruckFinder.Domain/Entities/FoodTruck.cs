using CsvHelper.Configuration.Attributes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodTruckFinder.Domain.Entities
{
    public class FoodTruck
    {
        [Name("locationid")]
        public string Locationid { get; set; }
        public string? Applicant { get; set; }
        public string? FacilityType { get; set; }

        [Name("cnn")]
        public string? Cnn { get; set; }
        public string? LocationDescription { get; set; }
        public string Address { get; set; }

        [Name("blocklot")]
        public string Blocklot { get; set; }
        
        [Name("block")]
        public string Block { get; set; }

        [Name("lot")]
        public string Lot { get; set; }

        [Name("permit")]
        public string Permit { get; set; }
        public string Status { get; set; }
        public string FoodItems { get; set; }
        public string X { get; set; }
        public string Y { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string Schedule { get; set; }

        [JsonPropertyName("dayshours")]
        public string Dayshours { get; set; }
        public string NOISent { get; set; }
        public string Approved { get; set; }
        public string Received { get; set; }
        public string PriorPermit { get; set; }
        public string ExpirationDate { get; set; }
        public string Location { get; set; }


    }
}
