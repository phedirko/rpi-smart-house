using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using RpiSmartHouse.Monitoring.Api.Services;

namespace RpiSmartHouse.Monitoring.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddSingleton<MessagePersistance>();
            services.AddTransient<IMqttClient>(c =>
            {
                return new MqttFactory().CreateMqttClient();
            });
            services.AddTransient<MQTTClient>(
                    c => new MQTTClient(
                        c.GetService<IMqttClient>(), 
                        c.GetService<IEventRepository>(),
                        "TEMP_TOPIC"));
            services.BuildServiceProvider().GetRequiredService<MQTTClient>();
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
