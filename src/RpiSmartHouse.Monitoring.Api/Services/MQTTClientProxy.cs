using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using RpiSmartHouse.Monitoring.Api.Contracts.Configuration;
using System;
using System.Threading.Tasks;

namespace RpiSmartHouse.Monitoring.Api.Services
{
    public class MQTTClientProxy
    {
        private readonly MQTTOptions _options;
        private readonly IMqttClient _mqttClient;
        private readonly IMqttClientOptions _clientOptions;

        public MQTTClientProxy(IOptions<MQTTOptions> options)
        {
            _options = options.Value;
            _mqttClient = new MqttFactory().CreateMqttClient();
            _clientOptions = new MqttClientOptionsBuilder()
                .WithWebSocketServer($"{_options.Host}:{_options.Port}")
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

    public class EvHandler
    {

    }
}
