using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RpiSmartHouse.Publisher
{
    public class Program
    {
        private static IMqttClient _mqttClient;

        static void Main(string[] args)
        {
            Task.Run(() => MainAsync()).Wait();
        }

        static async Task MainAsync()
        {
            var _mqttClient = new MqttFactory().CreateMqttClient();

            var options = new MqttClientOptionsBuilder()
                    .WithWebSocketServer("mqtt:9001/mqtt")
                    .Build();


            var connectionResult = await _mqttClient.ConnectAsync(options);

            while (true)
            {
                await _mqttClient.PublishAsync("temperature/kitchen-pub", $"Current temp is {DateTime.Now.Millisecond / 10} degrees");
                Console.WriteLine("Published!");
                Thread.Sleep(5001);
            }
        }
    }
}
