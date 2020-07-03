# Azure Championship 2020 Concept
FlowBio is a an example concept create for the Azure Championship 2020 competition. Details of which can be found here: https://certmatters.com/wp-content/uploads/2020/06/Azure-Fundamentals-Championship-Brief-030620.pdf 



## CoviFlo
[FlowBio Demo WebApp](https://azurechampionship.azurewebsites.net) is an Azure hosted web app using data ingested from simulated sensors (or this can also be any IoT sensor or MxChip for example).

Webapp link: https://azurechampionship.azurewebsites.net/?handler=start

The FlowBio concept is about bringing buildings to life and allowing them to have (through IoT and Cloud) their own experiences that support and compliment the users inside.

This particular concept is based around moving contaminated air out of harms way and out of the building as quickly as possible. The concept achieves this by using IoT devices to monitor and provision extraction, air conditioning units as well as entry and exit points to the building. Effectively trying to mimic an immune system response to dirty air. 

This sensor data is represented on the web app using shapes and colours to show what locations of the hospital (show as a 2D map) has contaminated air. Red represents high levels of contamination (driven form the sensor data) and green low levels of contamination. Planning and mapping the extraction of air is shown using A* pathfinding algorithm. This is not directly implemented in the solution but shows one example of planning a route to egress air keeping in mind obstacles (such as people) present in the area. 

The FlowBio web app can be accessed from any device anywhere in the world [here](https://azurechampionship.azurewebsites.net).

## Using Azure
Azure is used to fully host, manage, connect and monitor all devices and databases in this concept. Trigger functions have been employed to take ingested data and move into a CosmosDB. HTTP functions have then been used to create a RESTful API so that the data can be used by any interested external party. This web app uses the .NET Core infrastructure (using Azure WebApp services) as well as general web app hosting using the hosting provision tools. 

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

* [RestSharp](https://restsharp.dev) - The most popular REST API client library for .NET

* [Dr Derek Foster](https://staff.lincoln.ac.uk/d49a8e50-b321-48b7-9c33-3ad880bd18d6) - For the really great IoT Module that introduced Azure as well as much more but also provided the code examples and workshops to allow us to create the Triggers and RESTful API's used in this project.

* [jimbobbennett](https://github.com/jimbobbennett/MXChip-Workshop) - For the fantastic MxChip workshops and detailed overview of the Azure framework.

* [Clément Mihailescu](https://clementmihailescu.github.io/Pathfinding-Visualizer/) - For creating the really nice Pathfinding Visualizer tool. Many really nice examples for all the pathfinding algorithms you can ever think of. 

