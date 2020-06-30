using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.Azure.Devices.Client;
using Newtonsoft.Json;
using System.Text;
using System.Threading;
using System.IO;
using System.Data.Odbc;
using RestSharp;
using Newtonsoft.Json.Linq;


namespace AzureChampionship.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }


        //Starts the simulation
        public IActionResult OnPostStart()
        {

            //simulation connections
            Console.WriteLine("\nIoT Hub Simulated Sensor Data Has started:");

            FetchApiDataAsync();

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();

            return Page();
        }


        //instantaites devvice client string connection
        private static DeviceClient s_deviceClient;

        // The device connection string to authenticate the device with your IoT hub.
        private readonly static string s_connectionString = "HostName=gclawsonTempSensorHub.azure-devices.net;DeviceId=gclawson_mxchip;SharedAccessKey=egxteVKHF5+oypQALbXImcKQL1RM0edtnKglT/RBmNM=";

        //Async method to send simulated telemetry - Async to allow other processes to take place during data send
        public async void SendDeviceToCloudMessagesAsync()
        {

            Random rand = new Random();
            // Initial telemetry values
            double minRoom = 20;

            int i = 0;

            while (true)
            {

                i++;

                double currentRoom1 = minRoom + rand.NextDouble() * 11;
                double currentRoom2 = minRoom + rand.NextDouble() * 12;
                double currentRoom3 = minRoom + rand.NextDouble() * 13;
                double currentRoom4 = minRoom + rand.NextDouble() * 14;
                double currentRoom5 = minRoom + rand.NextDouble() * 15;
                double currentRoom6 = minRoom + rand.NextDouble() * 16;
                double currentRoom7 = minRoom + rand.NextDouble() * 15;
                double currentRoom8 = minRoom + rand.NextDouble() * 14;
                double currentRoom9 = minRoom + rand.NextDouble() * 13;
                double currentRoom10 = minRoom + rand.NextDouble() * 12;
                double currentRoom11 = minRoom + rand.NextDouble() * 11;
                double currentRoom12 = minRoom + rand.NextDouble() * 10;

                // Create JSON message
                var telemetryDataPoint = new
                {
                    room1 = currentRoom1,
                    room2 = currentRoom2,
                    room3 = currentRoom3,
                    room4 = currentRoom4,
                    room5 = currentRoom5,
                    room6 = currentRoom6,
                    room7 = currentRoom7,
                    room8 = currentRoom8,
                    room9 = currentRoom9,
                    room10 = currentRoom10,
                    room11 = currentRoom11,
                    room12 = currentRoom12,

                };

                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("room1Alert", (currentRoom1 > 30) ? "true" : "false");

                // Send the telemetry message
                await s_deviceClient.SendEventAsync(message);
                //Console.WriteLine("\n{0} > Sending Message: {1}", DateTime.Now, messageString);

 
                if (i >= 1) // number of iterations of data send
                {
                    //Console.WriteLine("Completed sending messages\n");
                    break;
                }

                await Task.Delay(1000); //1000 = 1 second delay

            }

        }

        public async void FetchApiDataAsync()
        {
            int i = 0;

            while (true)
            {

                //fetches sensor data from the RESTful API connectde to the CosmosDB
                FetchData();

                i++;

                if (i >= 1) // number of iterations of data send
                {
                    Console.WriteLine("\nIoT Hub Simulated Sensor Data Has stopped.\n");
                    break;
                }

                await Task.Delay(1000); //1000 = 1 second delay

            }

        }

        //Parses fetched json and dipslays selected elements
        public void FetchData()
        {

            //RESTful API url where our sensor data sits
            string url = "https://gclawsontempsensor.azurewebsites.net/api/temperature/%7Bdevicename%7D";

            //Use Restsharp to fetch data form url 
            string data = GetReleases(url);
            //Console.WriteLine(data);

            var newString = data.Substring(1, data.Length - 2); //removes sqwuare btrackets from front and end of json 
            //Console.WriteLine("\n\n\n{0}", newString);

            //pares json into c# object so we can pick elements from it to display
            JObject stuff = JObject.Parse(newString);
            //Console.WriteLine("\n\n\n{0}", stuff);

            //Select element from json object to display
            string room1 = (string)stuff["Temperature_Data"]["room1"];
            Console.WriteLine("\nRoom 1: {0}", room1);
            ViewData["room1SensorValue"] = room1;

            string room2 = (string)stuff["Temperature_Data"]["room2"];
            Console.WriteLine("Room 2: {0}", room2);
            ViewData["room2SensorValue"] = room2;

            string room3 = (string)stuff["Temperature_Data"]["room3"];
            Console.WriteLine("Room 3: {0}", room3);
            ViewData["room3SensorValue"] = room3;

            string room4 = (string)stuff["Temperature_Data"]["room4"];
            Console.WriteLine("Room 4: {0}", room4);
            ViewData["room4SensorValue"] = room4;

            string room5 = (string)stuff["Temperature_Data"]["room5"];
            Console.WriteLine("Room 5: {0}", room5);
            ViewData["room5SensorValue"] = room5;

            string room6 = (string)stuff["Temperature_Data"]["room6"];
            Console.WriteLine("Room 6: {0}", room6);
            ViewData["room6SensorValue"] = room6;

            string room7 = (string)stuff["Temperature_Data"]["room7"];
            Console.WriteLine("Room 7: {0}", room7);
            ViewData["room7SensorValue"] = room7;

            string room8 = (string)stuff["Temperature_Data"]["room8"];
            Console.WriteLine("Room 8: {0}", room8);
            ViewData["room8SensorValue"] = room8;

            string room9 = (string)stuff["Temperature_Data"]["room9"];
            Console.WriteLine("Room 9: {0}", room9);
            ViewData["room9SensorValue"] = room9;

            string room10 = (string)stuff["Temperature_Data"]["room10"];
            Console.WriteLine("Room 10: {0}", room10);
            ViewData["room10SensorValue"] = room10;

            string room11 = (string)stuff["Temperature_Data"]["room11"];
            Console.WriteLine("Room 11: {0}", room11);
            ViewData["room11SensorValue"] = room11;

            string room12 = (string)stuff["Temperature_Data"]["room12"];
            Console.WriteLine("Room 12: {0}", room12);
            ViewData["room12SensorValue"] = room12;

        }


        //Fecthes sensor data from RESTful API and returns reponse
        public static string GetReleases(string url)
        {
            var client = new RestClient(url);

            var response = client.Execute(new RestRequest());

            return response.Content;
        }

    }
    
}
