using FoodTruckFinder.Domain.Contracts;
using FoodTruckFinder.Domain.Dtos;
using FoodTruckFinder.Domain.Entities;
using FoodTruckFinder.Domain.Exceptions;
using System;

namespace FoodTruckFinder.Services
{
    public class FoodTruckService : IFoodTruckService
    {
        private IDataSourceProvider _dataSourceProvider { get; set; }
        private ApplicationConfig _applicationConfig { get; set; }
        public FoodTruckService(IDataSourceProvider dataSourceProvider, ApplicationConfig applicationConfig)
        {
            _dataSourceProvider = dataSourceProvider;
            _applicationConfig = applicationConfig;
        }

        public List<FoodTruck> Get(double latitude, double longitude, string? foodSearch, string type = "Truck")
        {
            if (latitude < -90 || latitude > 90)
            {
                throw new BadRequestExceptions("INVALID_LATITUDE");
            }

            if (longitude < -180 || longitude > 180)
            {
                throw new BadRequestExceptions("INVALID_LONGITUDE");
            }
            var distanceFromMedianLocation = CalculateDistance(latitude, longitude,
                new FoodTruck()
                {
                    Latitude = _applicationConfig.MedianLocation.Latitude,
                    Longitude = _applicationConfig.MedianLocation.Longitude
                });

            if (distanceFromMedianLocation.Item1 > _applicationConfig.MedianLocation.DistanceInKms)
            {
                throw new BadRequestExceptions("NOT_WITHIN_RANGE");
            }
            var results = _dataSourceProvider.FoodTrucks.Where(ft => ft.FacilityType == type && ft.Status == "APPROVED"
                           && (string.IsNullOrEmpty(foodSearch) || ft.FoodItems.Contains(foodSearch)))
                           .Select(ft => CalculateDistance(latitude, longitude, ft))
                           .OrderBy(v => v.Item1)
                           .Take(_applicationConfig.MinRecordsToView);

            return results.Select(r => r.Item2).ToList();
        }

        public List<FoodTruck> GetByAddress(string search, string type = "Truck")
        {
            if (string.IsNullOrEmpty(search))
            {
                throw new BadRequestExceptions("SEARCH_CODE_REQUIRED");
            }

            var foodTucks = _dataSourceProvider.FoodTrucks.
                            Where(ft => ft.Address.Contains(search)
                            && ft.FacilityType == type && ft.Status == "APPROVED").
                            Take(_applicationConfig.MinRecordsToView).ToList();
            return foodTucks;
        }

        /// <summary>
        /// Haversine formulla to calculate distance in kilometers
        /// </summary>
        /// <param name="rlatUser"></param>
        /// <param name="longitudeUser"></param>
        /// <param name="foodTruck"></param>
        /// <returns></returns>

        public Tuple<double, FoodTruck> CalculateDistance(double rlatUser, double longitudeUser, FoodTruck foodTruck)
        {
            double R = 6371;
            var lat = (rlatUser - foodTruck.Latitude).ToRadians();
            var lng = (longitudeUser - foodTruck.Longitude).ToRadians();
            var h1 = Math.Sin(lat / 2) * Math.Sin(lat / 2) +
                          Math.Cos(rlatUser.ToRadians()) * Math.Cos(foodTruck.Latitude.ToRadians()) *
                          Math.Sin(lng / 2) * Math.Sin(lng / 2);
            var h2 = 2 * Math.Asin(Math.Min(1, Math.Sqrt(h1)));
            return Tuple.Create(R * h2, foodTruck);
        }
    }
}