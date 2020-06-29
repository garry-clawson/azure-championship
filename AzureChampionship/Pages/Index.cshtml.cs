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
            Console.WriteLine("IoT Hub Simulated Sensor Data Has started:\n");

            // Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();

            Console.WriteLine("Send to device has completed. Async readings will now run.\n");

            return Page();
        }

        //Stops the Simulation
        public IActionResult OnPostStop()
        {

            //// Connect to the IoT hub using the MQTT protocol
            s_deviceClient = DeviceClient.CreateFromConnectionString(s_connectionString, TransportType.Mqtt);
            SendDeviceToCloudMessagesAsync();

            Console.WriteLine("\nSimulation has ended.\n");

            return Page();
        }


        //Code that sends messages to my Azure hub

        private static DeviceClient s_deviceClient;

        // The device connection string to authenticate the device with your IoT hub.

        private readonly static string s_connectionString = "HostName=gclawsonTempSensorHub.azure-devices.net;DeviceId=gclawson_mxchip;SharedAccessKey=egxteVKHF5+oypQALbXImcKQL1RM0edtnKglT/RBmNM=";

        //Async method to send simulated telemetry
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

                // Create JSON message
                var telemetryDataPoint = new
                {
                    room1 = currentRoom1,
                    room2 = currentRoom2,
                    room3 = currentRoom3,
                    room4 = currentRoom4,
                    room5 = currentRoom5,
                };
                var messageString = JsonConvert.SerializeObject(telemetryDataPoint);
                var message = new Message(Encoding.ASCII.GetBytes(messageString));

                // Add a custom application property to the message.
                // An IoT hub can filter on these properties without access to the message body.
                message.Properties.Add("room1Alert", (currentRoom1 > 30) ? "true" : "false");

                // Send the telemetry message
                await s_deviceClient.SendEventAsync(message);
                Console.WriteLine("\n{0} > Sending Message: {1}", DateTime.Now, messageString);

                if (i > 10)
                {
                    Console.WriteLine("\nCompleted 10 messages\n");
                    break;
                }

                await Task.Delay(1000);

            }

        }

    }
    
}
