using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace RpiSmartHouse.Publisher
{
    public class Program
    {
        static void Main(string[] args)
        {
            Task.Run(async () => await MainAsync()).Wait();
        }

        static async Task MainAsync()
        {
            var dispatcher = new Dispatcher(new MQTTClientProxy());
            await dispatcher.Start();

            await dispatcher.RunInternal();

            while (true)
            {
                foreach(var sensor in Config.Default.Sensors)
                {
                    string topic = $"{sensor.Room}/{sensor.Type}/{sensor.Identifier}-pub";
                    string tics = DateTime.Now.Ticks.ToString();
                    await dispatcher.SendMessage(topic, $"{topic}: {tics.Substring(tics.Length - 2)}");
                }                
            }
        }
    }
}
