using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MQTTnet;
using MQTTnet.Client;
using RpiSmartHouse.Monitoring.Api.Contracts.Configuration;
using RpiSmartHouse.Monitoring.Api.Extensions;
using RpiSmartHouse.Monitoring.Api.Services;
using Serilog;

namespace RpiSmartHouse.Monitoring.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();

            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public AppConfig _appConfig { get; }

        public MQDispatcher mqDispatcher;

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMqtt();

            services.AddTransient<IEventRepository, EventRepository>();
            services.AddSingleton<MessagePersistance>();

            services.AddLogging(lb => lb.AddSerilog(dispose: true));
            services.AddOptions();
            services.Configure<AppConfig>(Configuration);
            mqDispatcher = services.GetMQDispatcher();
            mqDispatcher.Start();
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
