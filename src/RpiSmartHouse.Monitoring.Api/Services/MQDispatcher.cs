using Microsoft.Extensions.Options;
using MQTTnet;
using MQTTnet.Client;
using RpiSmartHouse.Monitoring.Api.Contracts.Configuration;
using RpiSmartHouse.Monitoring.Api.Extensions;
using RpiSmartHouse.Monitoring.Api.Services.Persistance;
using Serilog;
using System.Text;
using System.Threading.Tasks;

namespace RpiSmartHouse.Monitoring.Api.Services
{
    public class MQDispatcher
    {
        private readonly MQTTClientProxy _mqttClientProxy;
        private readonly IEventRepository _eventRepository;
        private readonly SensorData _sensorData;

        private bool clientConnected = false;
        public MQDispatcher(MQTTClientProxy mqttClientProxy, IEventRepository eventRepository, IOptions<SensorData> options)
        {
            _mqttClientProxy = mqttClientProxy;
            _eventRepository = eventRepository;
            _sensorData = options.Value;
        }

        public void Start()
        {
            Task.Run(async () => await RunInternal());
        }

        private async Task RunInternal()
        {
            await _mqttClientProxy.Connect();
            System.Console.WriteLine("connected");
            await Subscribe();

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
            Log.Information($"[Received message]:");
            Log.Information($"[Topic]: {eventArgs.ApplicationMessage.Topic}");
            Log.Information($"[Payload]: {eventArgs.ApplicationMessage.ConvertPayloadToString()}");

            string text = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);

            _eventRepository.Add(text);
        }

        private async Task HandleDisconnected(MqttClientDisconnectedEventArgs eventArgs)
        {
            Log.Warning("Client disconnected");
            clientConnected = eventArgs.ClientWasConnected;
        }

        private async Task Subscribe()
        {
            foreach (string topic in _sensorData.GetTopics())
                await _mqttClientProxy.Subscribe(topic);
        }

        public async Task SendMessage(string topic, string message)
        {
            await _mqttClientProxy.SendMessage(topic, message);
        }
    }
}
