using System;
using System.IO;
using System.Threading;
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
    public class PlantSimulatorHostBuilder
    {
        private IParameters Parameters { get; }

        private IHostBuilder Host { get; set; }

        private CancellationTokenSource TokenSource { get; }

        public PlantSimulatorHostBuilder(string[] args)
        {
            TokenSource = new CancellationTokenSource();
            Parameters = new ParameterParser().Parse(args);
        }

        public PlantSimulatorHostBuilder Run()
        {
            Host.RunConsoleAsync(TokenSource.Token);
            return this;
        }

        public PlantSimulatorHostBuilder Build()
        {
            Host = Microsoft.Extensions.Hosting.Host.CreateDefaultBuilder()
                .UseSerilog(ConfigureLogging())
                .ConfigureServices(ConfigureServices);
            return this;
        }

        private void ConfigureServices(HostBuilderContext context, IServiceCollection collection)
        {
            collection.AddSingleton(provider => Parameters);
            collection.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            collection.AddSingleton<ISimulator, Simulation.PlantSimulator>();
            collection.AddScoped<IAsyncRuntime, PlantSimulatorRuntime>();
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

    }
}