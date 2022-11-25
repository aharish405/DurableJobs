using DurableJobs.Factory;
using Microsoft.Azure.WebJobs.Extensions.DurableTask;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.IO;
using System.Threading.Tasks;

namespace DurableJobs
{
    public class Program
    {
        static async Task Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var builder = new HostBuilder();
            builder.ConfigureWebJobs(config =>
            {
                config.AddAzureStorageCoreServices();
                config.AddServiceBus();
                config.AddTimers();
                config.AddDurableTask(options =>
                {
                    options.HubName = "JobHub";
                    //options.StorageProvider["ConnectionStringName"] = "AzureStorage";
                });
            });
            builder.ConfigureLogging((context, b) =>
            {
                b.AddConsole();
            });
            builder.ConfigureAppConfiguration(config =>
            {
                config.SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true).AddEnvironmentVariables();

            });
            builder.ConfigureServices((context, services) =>
            {
                services.AddTransient<IOrchestrator, CreateOrchestrator>()
                .AddTransient<IOrchestrator, UpdateOrchestrator>()
                .AddTransient<IFactory, Factory.Factory>();
            });

            var host=builder.Build();
            using (host)
            {
                await host.RunAsync();
            }
        }
    }
}
