# Azure Chapmionship 2020 Concept
CoviFlo is a an example concept create for the Azure Champiionship 2020 competion. Deatsil of which can be found here: https://certmatters.com/wp-content/uploads/2020/06/Azure-Fundamentals-Championship-Brief-030620.pdf 

## CoviFlo
[CoviFlow Demo WebApp](https://azurechampionship.azurewebsites.net) is a Azure hosted web app using data ingested from simulated sensors (or this can also be any IoT sensor or MxChip for example). 

The CoviFlo concept is about brining buildings ot live and allowing them to have (thorugh IoT and Cloud) their own experinces that support and compliment the users inside.

This particular concpet is based around moving contaminted air out of harms way and out of the building as quickly as possible. The concept achieves this by using IoT devices to monitor and provision extarction, air conditioing units as well as entry and exit points to the building. Effectivly trying to mimic an immune system reponse to dirty air. 

This sensor data is represented on the webapp using shapes and colours to show what locatsion of the hospital (show as a 2D map) has contaminated air. Red represents high levels of contamination (driven form the sensor data) and green low levels of contaminsation. Plannijng and mapping the extraction of air is shown using A* pathfinding algorythm. This is not doirectly impleimted in the solution but shows one example of planning a route to egress air keeping in mind obsticles (such as people) present in the area. 

The CoviFlo webapp can be accessed from any device anywhere in the world [here](https://azurechampionship.azurewebsites.net).

## Using Azure
Azure is used to fully host, manage, connect and monitor all devices and databases in this concept. Trigger functions have been employed to take ingested data and move into a CosmosDB. HTTP fucntions have then been used to create a RESTful API so that the data can be used by any interested external party. This web abb uses the .NET Core infrasture (using Azure WebApp services) as well asgeneral web app hosting using the hosting provison tools. 

### Installation

1. Clone the project

1. Download and install the latest release of the .NET runtime from [here](https://dotnet.microsoft.com/download).

1. Open up a terminal and navigate to the projects directory.

1. Run the command: ```dotnet run```

1. Using your browser visit: ```localhost:5001```
    
Thats it, you should then be good to go!

## Acknowledgements
* [ASP.NET](https://dotnet.microsoft.com/apps/aspnet) - Microsoft's open source framework for building web apps and services with .NET and C#.
* [Json.NET](https://www.newtonsoft.com/json) - A high-performance JSON framework for .NET, used for importing and processing user datasets in the full app.
