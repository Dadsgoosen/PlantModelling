using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;
using PlantSimulator.Runtime.Parameters;
using Serilog;
using Serilog.Core;

namespace PlantSimulator.Runtime
{
    public class PlantSimulatorHostBuilder
    {
        public static IHostBuilder Build(string[] args)
        {
            IParameters parameters = new ParameterParser().Parse(args);

            return Host.CreateDefaultBuilder(args)
                .UseSerilog(ConfigureLogging(null))
                .ConfigureServices((context, collection) => ConfigureServices(context, collection, parameters));
        }

        private static void ConfigureServices(HostBuilderContext context, IServiceCollection collection, IParameters parameters)
        {
            collection.AddSingleton(provider => parameters);
            collection.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            collection.AddScoped<IAsyncRuntime, PlantSimulator>();
            collection.AddHostedService<PlantSimulatorHost>();
        }

        private static Logger ConfigureLogging(IParameters settingsFile)
        {
            IConfiguration configuration = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile(settingsFile.SettingsPath, false, true)
               .Build();

            return new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger();
        }

    }
}