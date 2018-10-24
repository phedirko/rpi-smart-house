using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;

namespace RpiSmartHouse.Monitoring.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(c => c.AddJsonFile("appsettings.json"))
                .UseStartup<Startup>()
                .UseUrls("http://*:5001")
                .Build();
    }
}
