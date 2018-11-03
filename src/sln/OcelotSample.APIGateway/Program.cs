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
using Ocelot.Provider.Eureka;

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
                configBuilder.AddJsonFile("ocelot.configuration.dynamic.json");
            })
            .ConfigureServices((buildContext, services) =>
            {
                services.AddOcelot().AddEureka();
            })
            .UseStartup<Startup>()
            .Configure(app =>
            {
                app.UseOcelot().Wait();
            });
    }
}
