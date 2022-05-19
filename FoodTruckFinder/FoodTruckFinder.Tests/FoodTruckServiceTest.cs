using FoodTruckFinder.Data;
using FoodTruckFinder.Domain.Contracts;
using FoodTruckFinder.Domain.Entities;
using FoodTruckFinder.Domain.Exceptions;
using FoodTruckFinder.Services;
using Xunit;

namespace FoodTruckFinder.Tests
{
    public class FoodTruckServiceTest
    {
        public class Get
        {
            [Theory]
            [InlineData(1.22, 2.56, "123")]
            public void ShouldReturnNonEmptyListForValidParameters(double longitude, double latitude, string type)
            {
                IFoodTruckService foodTruckService = GetFoodTruckService();
                var result = foodTruckService.Get(longitude, latitude, null, type);
                Assert.NotNull(result);
            }

            [Fact]
            public void ValidateWhenPassIncorrectLatitude()
            {
                IFoodTruckService foodTruckService = GetFoodTruckService();
                Assert.Throws<BadRequestExceptions>(() => foodTruckService.Get(-100, 1.22, "Hot", "truck"));
            }

            [Fact]
            public void ValidateWhenPassIncorrectLongitiude()
            {
                IFoodTruckService foodTruckService = GetFoodTruckService();
                Assert.Throws<BadRequestExceptions>(() => foodTruckService.Get(1, 200, "Hot", "truck"));
            }

            [Fact]
            public void ValidateWhenCoordinatesAreOutsideCityLimit()
            {
                IFoodTruckService foodTruckService = GetFoodTruckService();
                Assert.Throws<BadRequestExceptions>(() => foodTruckService.Get(6.927079, 79.861243, "Hot", "truck"));
            }
        }

        public class CalculateDistance
        {
            [Theory]
            [InlineData(6.927079, 79.861243, 37.7749, -122.4194, 14533.957233064391)]
            [InlineData(6.567079, 80.811243, 37.7749, -122.4194, 14528.295621900668)]
            [InlineData(37.7749, -122.4194, 37.7749, -122.4194, 0)]
            public void ShouldReturnDistanceForValidParameters(double lat1, double lon1, double lat2, double lon2, double expected)
            {
                IFoodTruckService foodTruckService = GetFoodTruckService();
                var result = foodTruckService.CalculateDistance(lat1, lon1, new FoodTruck() { Latitude = lat2, Longitude = lon2 });
                Assert.Equal(expected, result.Item1);
            }
        }


        public static IFoodTruckService GetFoodTruckService()
        {
            var mockDataSourceProvider = new InMemoryDataContext();
            string[] domains = { "1", "2" };
            var appConfig = new Domain.Dtos.ApplicationConfig()
            {
                AllowedDomains = domains,
                MedianLocation = new Domain.Dtos.MedianLocation()
                {
                    DistanceInKms = 10,
                    Latitude = 37.7749,
                    Longitude = -122.4194
                }
            };

            var foodTruckService = new FoodTruckService(mockDataSourceProvider, appConfig);
            return foodTruckService;
        }


    }
}