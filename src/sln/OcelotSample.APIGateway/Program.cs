using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace OcelotSample.APIGateway
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureAppConfiguration((configBuilder) =>
            {
                configBuilder.AddJsonFile("ocelot.configuration.json");
            })
            .ConfigureServices((buildContext, services) =>
            {
                services.AddOcelot();
            })
            .UseStartup<Startup>()
            .Configure(app =>
            {
                app.UseOcelot().Wait();
            });
    }
}
