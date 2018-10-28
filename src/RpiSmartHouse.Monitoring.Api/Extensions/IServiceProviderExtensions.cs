using Microsoft.Extensions.DependencyInjection;
using RpiSmartHouse.Monitoring.Api.Services;

namespace RpiSmartHouse.Monitoring.Api.Extensions
{
    public static class IServiceProviderExtensions
    {
        public static void AddMqtt(this IServiceCollection services)
        {
            services.AddSingleton<MQTTClientProxy>();
            services.AddSingleton<MQDispatcher>();
        }

        public static MQDispatcher GetMQDispatcher(this IServiceCollection services)
        {
            return services.BuildServiceProvider().GetRequiredService<MQDispatcher>();
        }
    }
}
