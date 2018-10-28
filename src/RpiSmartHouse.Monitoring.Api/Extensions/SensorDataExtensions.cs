using RpiSmartHouse.Monitoring.Api.Contracts.Configuration;
using System.Collections.Generic;
using System.Linq;

namespace RpiSmartHouse.Monitoring.Api.Extensions
{
    public static class SensorDataExtensions
    {
        public static IEnumerable<string> GetTopics(this SensorData sensorData)
        {
            return sensorData.Sensors.Select(x => $"{x.Room}/{x.Type}/{x.Identifier}-pub");
        }
    }
}
