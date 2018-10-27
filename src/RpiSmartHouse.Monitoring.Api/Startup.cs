using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using RpiSmartHouse.Monitoring.Api.Contracts.Configuration;
using RpiSmartHouse.Monitoring.Api.Services;
using Serilog;

namespace RpiSmartHouse.Monitoring.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public AppConfig _appConfig { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IEventRepository, EventRepository>();
            services.AddSingleton<MessagePersistance>();
            services.AddTransient<IMqttClient>(c =>
            {
                return new MqttFactory().CreateMqttClient();
            });
            services.AddTransient<MQTTClient>(c => new MQTTClient(c.GetService<IMqttClient>(), c.GetService<IEventRepository>(),"TEMP_TOPIC"));
            services.BuildServiceProvider().GetRequiredService<MQTTClient>();
            services.AddLogging(lb => lb.AddSerilog(dispose: true));
            services.AddOptions();
            services.Configure<AppConfig>(Configuration);
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            Log.Logger = new LoggerConfiguration()
              .Enrich.FromLogContext()
              .WriteTo.Console()
              .CreateLogger();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseMvc();
        }
    }
}
