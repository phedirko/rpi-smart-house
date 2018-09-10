using MQTTnet;
using MQTTnet.Client;
using System.Text;

namespace RpiSmartHouse.Monitoring.Api.Services
{
    public class MQTTClient
    {
        private readonly IMqttClient _mqttClient;
        private readonly IEventRepository _eventRepository;
        private readonly string _topic;

        public MQTTClient(IMqttClient mqttClient, IEventRepository eventRepository, string topic)
        {
            _mqttClient = mqttClient;
            _eventRepository = eventRepository;
            _topic = topic;

            var options = new MqttClientOptionsBuilder()
                .WithWebSocketServer("localhost:9001/mqtt")
                .Build();

            _mqttClient.ConnectAsync(options).GetAwaiter().GetResult();
            _mqttClient.SubscribeAsync(_topic).GetAwaiter().GetResult();

            _mqttClient.ApplicationMessageReceived += UpdateStatus;
        }

        private void UpdateStatus(object sender, MqttApplicationMessageReceivedEventArgs eventArgs)
        {
            string text = Encoding.UTF8.GetString(eventArgs.ApplicationMessage.Payload);

            _eventRepository.Add(text);
        }
    }
}
