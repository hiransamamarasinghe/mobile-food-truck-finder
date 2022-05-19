# mobile-food-truck-finder
---
This is the main repository for the server api!

Please read the information below at least once to understand how to work with this project.

## Prerequisites
---
* .NET Core 6.0 sdk
* Microsoft Visual studio or VS code

## Application Config
---
 * MedianLocation - This setting contains city coordinates and the max distance between two coordinates in the city. For instance
   "MedianLocation": {
    "Latitude": 37.7749,
    "Longitude": -122.4194,
    "DistanceInKms": 15
  },

* AllowedDomains - Governs the CORS policy. This allow which domains could request data
* MinRecordsToView - Governs minimum search records count to return from a query

## Build and deployment
* use "dotnet build" command to build the application
* use "dotnet test FoodTruckFinder.Tests" to run the unit test project which is FoodTruckFinder.Tests
* use "dotnet publish" to publish the application and its dependencies
