using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;
using PlantSimulator.Runtime.Parameters;
using PlantSimulator.Simulation;
using Serilog;
using Serilog.Core;

namespace PlantSimulator.Runtime
{
    public class PlantSimulatorHostBuilder : IDisposable
    {
        private IParameters Parameters { get; }

        private IHostBuilder HostBuilder { get; set; }

        public PlantSimulatorHostBuilder(string[] args)
        {
            Parameters = new ParameterParser().Parse(args);
        }

        public async Task Run()
        {
            await Host.CreateDefaultBuilder()
                .UseSerilog(ConfigureLogging())
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection collection)
        {
            collection.AddSingleton(provider => Parameters);
            collection.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            collection.AddSingleton<ISimulator, Simulation.PlantSimulator>();
            collection.AddScoped<IRuntime, Runtime>();
            collection.AddHostedService<PlantSimulatorHost>();
        }

        private Logger ConfigureLogging()
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(Parameters.SettingsPath, false, true)
               .Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

        public void Dispose()
        {
        }
    }
}