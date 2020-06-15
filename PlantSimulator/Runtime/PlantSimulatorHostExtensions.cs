using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Logging;
using PlantSimulator.Outputs;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Corn;
using PlantSimulator.Simulation.PlantParts.Helpers;
using Serilog;

namespace PlantSimulator.Runtime
{
    public static class PlantSimulatorHostExtensions
    {
        public static IServiceCollection AddPlantSimulator(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<PlantSimulationOptions>(configuration.GetSection("Simulation"));
            service.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            service.AddSingleton<IPlantSimulatorOptionsService, PlantSimulatorOptionsService>();
            service.AddTransient<IPlantDescriptorService, PlantDescriptorService>();

            service.AddCornPlant();

            service.AddSingleton<SimulationEnvironment>();
            service.AddTransient<IGeometryHelper, GeometryHelper>();
            service.AddTransient<ICellCollisionDetection, CellCollisionDetection>();
            service.AddTransient<ICellBodySystemSolver, GenericCellBodySystemSolver>();
            service.AddTransient<IPlantRunner, GenericPlantRunner>();
            service.AddTransient<IPlantGrower, GenericPlantGrower>();
            service.AddTransient<ICellGrower, GenericCellGrower>();
            service.AddTransient<ISimulationStateFactory, SimulationStateFactory>();
            service.AddSingleton<IRuntimeBroker<Simulation.PlantSimulator>, SimulationRuntimeBroker>();
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