using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Logging;
using PlantSimulator.Runtime.Parameters;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Options;
using Serilog;
using Serilog.Core;

namespace PlantSimulator.Runtime
{
    public class PlantSimulatorHostBuilder : IDisposable
    {
        private IParameters Parameters { get; }

        private IHostBuilder HostBuilder { get; set; }

        private IConfiguration Configuration { get; set; }

        public PlantSimulatorHostBuilder(string[] args)
        {
            Parameters = new ParameterParser().Parse(args);
            ConfigureConfiguration();
        }

        private void ConfigureConfiguration()
        {
            Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Parameters.SettingsPath, false, true)
                .Build();
        }

        public async Task Run()
        {
            await Host.CreateDefaultBuilder()
                .ConfigureAppConfiguration(builder => builder.AddConfiguration(Configuration))
                .UseSerilog(ConfigureLogging())
                .ConfigureServices(ConfigureServices)
                .RunConsoleAsync();
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection collection)
        {
            collection.Configure<SimulationOptions>(Configuration.GetSection("Simulation"));
            collection.AddSingleton(provider => Parameters);
            collection.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            collection.AddSingleton<ISimulator, Simulation.PlantSimulator>();
            collection.AddScoped<IRuntime, Runtime>();
            collection.AddHostedService<PlantSimulatorHost>();
        }

        private Logger ConfigureLogging()
        {
            return new LoggerConfiguration()
                .ReadFrom.Configuration(Configuration)
                .CreateLogger();
        }

        public void Dispose()
        {
        }
    }
}