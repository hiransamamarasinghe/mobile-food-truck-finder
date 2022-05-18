using FoodTruckFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckFinder.Domain.Contracts
{
    public interface IDataSourceProvider
    {
        public List<FoodTruck> FoodTrucks { get; }
    }
}
