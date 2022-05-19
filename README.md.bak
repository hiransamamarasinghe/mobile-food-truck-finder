# mobile-food-truck-finder
---
This is the main repository for the server api!

Please read the information below at least once to understand how to work with this project.

## Prerequisites

* .NET Core 6.0 sdk
* Microsoft Visual studio or VS code

## Application Config
 * MedianLocation - This setting contains city coordinates and the max distance between two coordinates in the city. For instance
   "MedianLocation": {
    "Latitude": 37.7749,
    "Longitude": -122.4194,
    "DistanceInKms": 15
  },
  "DataSource": {
    "Name": "CSV",
    "Path": "Mobile_Food_Facility_Permit.csv"
  }

* AllowedDomains - Governs the CORS policy. This allow which domains could request data
* MinRecordsToView - Governs minimum search records count to return from a query
* DataSource - Configures the datasource dynamically. SET CSV for CSV source and path as the CSV file path

## Build and deployment without docker
* use "dotnet build" command to build the application
* use "dotnet test FoodTruckFinder.Tests" to run the unit test project which is FoodTruckFinder.Tests
* use "dotnet publish" to publish the application and its dependencies

## Build and deployment with docker
** Important - This project is configired with Docker to run as a container app. However, this needs to be further tested 
* Use the command line and change the current directory to root folder of the application 
* Issue the command docker build -t foodtruckapi -f FoodTruckFinder.Api/Dockerfile .