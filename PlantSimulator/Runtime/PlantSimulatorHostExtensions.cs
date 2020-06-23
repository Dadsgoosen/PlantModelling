using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Logging;
using PlantSimulator.Outputs;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.Operations.Development;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Corn;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Helpers;
using Serilog;
using RootPartDevelopment = PlantSimulator.Simulation.Operations.Development.RootPartDevelopment;

namespace PlantSimulator.Runtime
{
    public static class PlantSimulatorHostExtensions
    {
        public static IServiceCollection AddPlantSimulator(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<PlantSimulationOptions>(configuration.GetSection("Simulation"));
            service.AddTransient(typeof(ILoggerAdapter<>), typeof(LoggerAdapter<>));
            service.AddSingleton<IPlantSimulatorOptionsService, PlantSimulatorOptionsService>();
            service.AddSingleton<IPlantDescriptorService, PlantDescriptorService>();
            service.AddSingleton<ISingularCellCreator, HexagonCellCreator>();
            service.AddSingleton<IPlantPartCellCreator, PlantPartCellCreator>();

            service.AddCornPlant();

            // Collision, geometry and Body System
            service.AddSingleton<IGeometryHelper, GeometryHelper>();
            service.AddSingleton<ICellSizer, GenericCellSizer>();
            service.AddSingleton<ICellCollisionDetection, CellCollisionDetection>();
            service.AddSingleton<ICellBodySystemSolver, GenericCellBodySystemSolver>();

            // Plant part and cell factories
            service.AddSingleton<ICellFactory, GenericCellFactory>();
            service.AddSingleton<ICellGridFactory, HexagonalCellGridFactory>();
            service.TryAddSingleton<IInternodePartFactory, GenericInternodePartFactory>();
            service.TryAddSingleton<IStemPartFactory, GenericStemPartFactory>();
            service.TryAddSingleton<INodePartFactory, GenericNodePartFactory>();
            service.TryAddSingleton<IPetiolePartFactory, GenericPetiolePartFactory>();
            service.TryAddSingleton<IPlantPartCellCreator, PlantPartCellCreator>();
            service.TryAddSingleton<IRootPartFactory, GenericRootPartFactory>();

            // Development
            service.AddSingleton<IPlantPartDevelopment<Internode>, InternodePartDevelopment>();
            service.AddSingleton<IPlantPartDevelopment<Root>, RootPartDevelopment>();
            service.AddSingleton<IPlantPartDeveloper, PlantPartDeveloper>();

            // Growth
            service.AddSingleton<SimulationEnvironment>();
            service.AddSingleton<ICellDivider, GenericCellDivider>();
            service.AddSingleton<IPlantRunner, GenericPlantRunner>();
            service.AddSingleton<IPlantGrower, GenericPlantGrower>();
            service.AddSingleton<ICellGrower, GenericCellGrower>();
            service.AddSingleton<ISimulationStateFactory, SimulationStateFactory>();
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