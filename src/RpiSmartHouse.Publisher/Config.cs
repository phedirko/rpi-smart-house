using System.Collections.Generic;

namespace RpiSmartHouse.Publisher
{
    public class Config
    {
        public ICollection<Sensor> Sensors { get; set; }

        public static Config Default =>
            new Config
            {
                Sensors = new[]
                {
                    new Sensor
                    {
                        Room = "kitchen",
                        Type = "temperature",
                        Name = "Kitchen main",
                        Identifier = "temp_1"
                    },
                    new Sensor
                    {
                        Room = "kitchen",
                        Type = "temperature",
                        Name = "Kitchen sec",
                        Identifier = "temp_2"
                    },
                    new Sensor
                    {
                        Room = "bedroom",
                        Type = "temperature",
                        Name = "Bedroom main",
                        Identifier = "temp_1"
                    }
                }
            };
    }

    public class Sensor
    {
        public string Room { get; set; }

        public string Type { get; set; }

        public string Identifier { get; set; }

        public string Name { get; set; }
    }
}
