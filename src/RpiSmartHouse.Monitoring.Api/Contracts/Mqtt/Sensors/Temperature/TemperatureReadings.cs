using Newtonsoft.Json;

namespace RpiSmartHouse.Monitoring.Api.Contracts.Mqtt.Sensors.Temperature
{
    public class TemperatureReadings
    {
        [JsonProperty("temperature")]
        public double Temperature { get; set; }

        [JsonProperty("humidity")]
        public double Humidity { get; set; }
    }
}
