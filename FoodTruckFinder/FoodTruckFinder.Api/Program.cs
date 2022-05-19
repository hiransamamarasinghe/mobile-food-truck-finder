using FoodTruckFinder.Api.Middleware;
using FoodTruckFinder.Data;
using FoodTruckFinder.Domain.Contracts;
using FoodTruckFinder.Domain.Dtos;
using FoodTruckFinder.Domain.Entities;
using FoodTruckFinder.Services;

var builder = WebApplication.CreateBuilder(args);

double.TryParse(builder.Configuration.GetSection("MedianLocation")["Latitude"], out double medialLatitude);
double.TryParse(builder.Configuration.GetSection("MedianLocation")["Longitude"], out double medialLongitude);
int.TryParse(builder.Configuration.GetSection("MedianLocation")["DistanceInKms"], out int medialDistance);
var appConfig = new ApplicationConfig()
{
    MedianLocation = new MedianLocation()
    {
        Latitude = medialLatitude,
        Longitude = medialLongitude,
        DistanceInKms = medialDistance
    },
    AllowedDomains = builder.Configuration.GetSection("AllowedDomains").Get<string[]>(),
    MinRecordsToView = builder.Configuration.GetSection("MinRecordsToView").Get<int>(),
    DataSource = new DataSource()
    {
        Name = builder.Configuration.GetSection("DataSource")["Name"],
        Path = builder.Configuration.GetSection("DataSource")["Path"]
    }
};
builder.Services.AddSingleton(appConfig);

var allowedDomains = "_allowedDomains";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowedDomains,
                          policy =>
                          {
                              policy.WithOrigins(appConfig.AllowedDomains)
                                                  .AllowAnyHeader()
                                                  .AllowAnyMethod();
                          });
});
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IFoodTruckService, FoodTruckService>();
if (appConfig.DataSource.Name == "CSV")
{
    builder.Services.AddSingleton<IDataSourceProvider, CSVDataContext>();
}
else
{
    //Default to CSV to avoid run time failure.
    builder.Services.AddSingleton<IDataSourceProvider, CSVDataContext>();
}

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();

app.UseMiddleware<ErrorHandlerMiddleware>();
app.UseCors(allowedDomains);
app.UseHttpsRedirection();


app.MapGet("/FoodTruck/{latitude}/{longitude}/{foodsearch?}", (double latitude, double longitude, string? foodsearch, IFoodTruckService foodTruckService) =>
{
    return foodTruckService.Get(latitude, longitude, foodsearch);
})
.WithName("Get")
.Produces<List<FoodTruck>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

app.MapGet("/FoodTruck/{location}", (string location, IFoodTruckService foodTruckService) =>
{
    return foodTruckService.GetByAddress(location);
})
.WithName("GetByAddress")
.Produces<List<FoodTruck>>(StatusCodes.Status200OK)
.Produces(StatusCodes.Status400BadRequest);

app.Run();
