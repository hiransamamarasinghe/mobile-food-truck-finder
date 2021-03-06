using CsvHelper;
using CsvHelper.Configuration;
using FoodTruckFinder.Domain.Contracts;
using FoodTruckFinder.Domain.Dtos;
using FoodTruckFinder.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckFinder.Data
{
    public class CSVDataContext : IDataSourceProvider
    {
        private ApplicationConfig _applicationConfig;
        public CSVDataContext(ApplicationConfig applicationConfig)
        {
            _applicationConfig = applicationConfig;
        }

        private List<FoodTruck>? _foodTrucks { get; set; }
        public List<FoodTruck> FoodTrucks { 
            get
            {
                if (_foodTrucks == null)
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        DetectDelimiter = true,
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null
                    };

                    using (var reader = new StreamReader(_applicationConfig.DataSource.Path))
                    using (var csv = new CsvReader(reader, config))
                    {
                        _foodTrucks = csv.GetRecords<FoodTruck>().ToList();
                    }
                }
                return _foodTrucks;
            }
        }
    }
}
