using FoodTruckFinder.Domain.Dtos;
using FoodTruckFinder.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FoodTruckFinder.Domain.Contracts
{
    public interface IFoodTruckService
    {
        List<FoodTruck> Get(double latitude, double longitude, string? foodSearch, string type = "Truck");

        List<FoodTruck> GetByAddress(string city, string type = "Truck");
    }
}
