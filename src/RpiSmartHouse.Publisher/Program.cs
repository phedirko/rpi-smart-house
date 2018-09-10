using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RpiSmartHouse.Publisher
{
    class Program
    {
        private static IMqttClient _mqttClient;
        private static string TEMP_TOPIC = "TEMP_TOPIC";

        static void Main(string[] args)
        {
            MainAsync().GetAwaiter().GetResult();
        }

        static async Task MainAsync()
        {
            var _mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                    .WithWebSocketServer("localhost:9001/mqtt")
                    .Build();


            var connectionResult = await _mqttClient.ConnectAsync(options);

            while (true)
            {
                Thread.Sleep(750);
                await _mqttClient.PublishAsync(TEMP_TOPIC, $"Current temp is {DateTime.Now.Millisecond / 10} degrees");
                Console.WriteLine("Published!");
                Thread.Sleep(750);
            }
        }
    }
}
