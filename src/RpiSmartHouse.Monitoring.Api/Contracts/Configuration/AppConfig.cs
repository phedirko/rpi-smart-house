using System.Collections.Generic;

namespace RpiSmartHouse.Monitoring.Api.Contracts.Configuration
{
    public class AppConfig
    {        
        public ICollection<Network> Networks { get; set; }

        public SensorData SensorData { get; set; }

        public MQTTOptions MQTTOptions { get; set; }
    }

    public class MQTTOptions
    {
        public string Host { get; set; }

        public int Port { get; set; }
    }

    public class Network
    {
        public string Name { get; set; }

        public ICollection<Host> Hosts { get; set; }
    }

    public class Host
    {
        public string Name { get; set; }

        public string Address { get; set; }
    }

    public class SensorData
    {
        public ICollection<Sensor> Sensors { get; set; }
    }

    public class Sensor
    {
        public string Identifier { get; set; }

        public string Name { get; set; }
    }
}
