using FoodTruckFinder.Data;
using FoodTruckFinder.Domain.Contracts;
using FoodTruckFinder.Domain.Entities;
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
            public void ShouldReturnANonEmptyListForValidParameters(double longitude, double latitude, string type)
            {
                IFoodTruckService foodTruckService = GetFoodTruckService();
                var result = foodTruckService.Get(longitude, latitude, type);
                Assert.NotNull(result);
            }

            [Theory, MemberData(nameof(SplitCountData))]
            public void ValidateWhenPassIncorrectParameters(double longitude, double latitude, string type)
            {
            }
            private static IEnumerable<object[]> SplitCountData =>
            new List<object[]>
            {
                    new object[] {1.22, 2.56,new List<FoodTruck>() {
                    new FoodTruck()
                    {
                        Applicant = "The Geez Freeze",
                        Address = "123"
                    }
                    } },
            };


            IFoodTruckService GetFoodTruckService()
            {
                var mockDataSourceProvider = new InMemoryDataContext();
                var foodTruckService = new FoodTruckService(mockDataSourceProvider);
                return foodTruckService;
            }

        }
    }
}