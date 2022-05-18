using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FoodTruckFinder.Domain.Dtos
{
    public class User
    {
        [JsonPropertyName("Username")]
        public string UserName { get; set; }
        public string Identifier { get; set; }

        public string FirstName { get; set; }

        [JsonPropertyName("Lastname")]
        public string LastName { get; set; }

    }
}
