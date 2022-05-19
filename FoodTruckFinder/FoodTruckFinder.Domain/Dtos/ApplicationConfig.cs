using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckFinder.Domain.Dtos
{
    public class ApplicationConfig
    {
        public MedianLocation MedianLocation { get; set; }

        public string[] AllowedDomains { get; set; }

        public int MinRecordsToView { get; set; } = 10;

        public DataSource DataSource { get; set; }
    }

    public class DataSource
    {
        public string Name { get; set; }

        public string Path { get; set; }

    }

    public class MedianLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public int DistanceInKms { get; set; }
    }
}
