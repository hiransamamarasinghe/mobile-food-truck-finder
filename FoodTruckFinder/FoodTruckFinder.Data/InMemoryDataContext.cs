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
using System.Text.Json;
using System.Threading.Tasks;

namespace FoodTruckFinder.Data
{
    public class InMemoryDataContext : IDataSourceProvider
    {
        private List<FoodTruck>? _foodTrucks { get; set; }
        public List<FoodTruck> FoodTrucks { 
            get
            {
                if(_foodTrucks == null)
                {
                    var config = new CsvConfiguration(CultureInfo.InvariantCulture)
                    {
                        DetectDelimiter =true,
                        HasHeaderRecord = true,
                        HeaderValidated = null,
                        MissingFieldFound = null
                    };

                    using (var reader = new StreamReader("Store/Mobile_Food_Facility_Permit.csv"))
                    using (var csv = new CsvReader(reader, config))
                    {
                        var records = csv.GetRecords<FoodTruck>().ToList();
                        return records;
                    }
                }
                return _foodTrucks;
            }
        }


    }
}
