using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RpiSmartHouse.Monitoring.Api.Contracts.Configuration
{
    public class AppConfig
    {        
        public ICollection<Network> Networks { get; set; }

        public ICollection<Sensor> Sensors { get; set; }
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

    public class Sensor
    {
        public string Identifier { get; set; }

        public string Name { get; set; }
    }
}
