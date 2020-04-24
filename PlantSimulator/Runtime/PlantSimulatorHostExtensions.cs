using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;
using Serilog;

namespace PlantSimulator.Runtime
{
    public static class PlantSimulatorHostExtensions
    {
        public static IServiceCollection AddPlantSimulator(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<SimulationOptions>(configuration.GetSection("Simulation"));
            service.AddSingleton(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            service.AddSingleton<ISimulator, Simulation.PlantSimulator>();
            service.AddSingleton<IRuntimeBroker, SimulationRuntimeBroker>();
            service.AddSingleton<IRuntime, Runtime>();
            return service;
        }

        public static IServiceCollection AddPlantSimulatorHost(this IServiceCollection service)
        {
            return service.AddHostedService<PlantSimulatorHost>();
        }

        public static IServiceCollection AddPlantSimulatorRuntimeBroker(this IServiceCollection service)
        {
            return service.AddSingleton<SimulationRuntimeBroker>();
        }

        public static IHostBuilder AddPlantSimulatorLogging(this IHostBuilder builder, IConfiguration configuration)
        {
            return builder.UseSerilog(new LoggerConfiguration()
                .ReadFrom.Configuration(configuration)
                .CreateLogger());
        }
    }
}