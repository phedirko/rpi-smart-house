using MQTTnet;
using MQTTnet.Client;
using System;
using System.Threading.Tasks;

namespace RpiSmartHouse.Publisher
{
    public class MQTTClientProxy
    {
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _clientOptions;

        public MQTTClientProxy()
        {
            _mqttClient = new MqttFactory().CreateMqttClient();
            _clientOptions = new MqttClientOptionsBuilder()
                .WithWebSocketServer($"mqtt:9001")
                .Build();
        }

        public async Task Connect()
        {
            await _mqttClient.ConnectAsync(_clientOptions);
        }

        public void RegisterMessageHandler(Func<MqttApplicationMessageReceivedEventArgs, Task> handler)
        {
            _mqttClient.ApplicationMessageReceived += async (s, e) => await handler(e);
        }

        public void RegisterDisconnectedHandler(Func<MqttClientDisconnectedEventArgs, Task> handler)
        {
            _mqttClient.Disconnected += async (s, e) => await handler(e);
        }

        public async Task Disconnect()
        {
            await _mqttClient.DisconnectAsync();
        }

        public async Task Subscribe(string topic)
        {
            await _mqttClient.SubscribeAsync(topic);
        }

        public async Task SendMessage(string topic, string message)
        {
            await _mqttClient.PublishAsync(topic, message);
        }

        public bool IsConnected => _mqttClient.IsConnected;
    }
}