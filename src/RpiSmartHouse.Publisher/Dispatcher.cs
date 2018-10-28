using MQTTnet;
using MQTTnet.Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RpiSmartHouse.Publisher
{
    public class Dispatcher
    {
        private readonly MQTTClientProxy _mqttClientProxy;

        private bool clientConnected = false;
        public Dispatcher(MQTTClientProxy mqttClientProxy)
        {
            _mqttClientProxy = mqttClientProxy;
        }

        public Task Start()
        {
            return _mqttClientProxy.Connect();
        }

        public async Task RunInternal()
        {
            Console.WriteLine("Connected");
            //await Subscribe();

            _mqttClientProxy.RegisterMessageHandler(HandleMessage);
            _mqttClientProxy.RegisterDisconnectedHandler(HandleDisconnected);

            while (true)
            {
                if (!clientConnected)
                    await _mqttClientProxy.Connect();
            }
        }

        private async Task HandleMessage(MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            Console.WriteLine($"[Received message]:");
            Console.WriteLine($"[Topic]: {eventArgs.ApplicationMessage.Topic}");
            Console.WriteLine($"[Payload]: {eventArgs.ApplicationMessage.ConvertPayloadToString()}");

            string text = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);
        }

        private async Task HandleDisconnected(MqttClientDisconnectedEventArgs eventArgs)
        {
            Console.WriteLine("Client disconnected");
            clientConnected = eventArgs.ClientWasConnected;
        }

        //private async Task Subscribe()
        //{
        //    foreach (string topic in _sensorData.GetTopics())
        //        await _mqttClientProxy.Subscribe(topic);
        //}

        public async Task SendMessage(string topic, string message)
        {
            await _mqttClientProxy.SendMessage(topic, message);
        }
    }
}
