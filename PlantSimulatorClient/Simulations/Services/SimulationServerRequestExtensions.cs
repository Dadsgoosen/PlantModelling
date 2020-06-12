using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Options;
using PlantSimulatorClient.Simulations.Protos;
using SimulationOptions = PlantSimulator.Simulation.Options.SimulationOptions;
using GrpcSimulationOptions = PlantSimulatorClient.Simulations.Protos.SimulationOptions;
using SimulationPlantOptions = PlantSimulator.Simulation.Options.SimulationPlantOptions;
using GrpcSimulationPlantOptions = PlantSimulatorClient.Simulations.Protos.SimulationPlantOptions;
using SimulationEnvironmentOptions = PlantSimulator.Simulation.Options.SimulationEnvironmentOptions;
using GrpcSimulationEnvironmentOptions = PlantSimulatorClient.Simulations.Protos.SimulationEnvironmentOptions;
using IntRange = PlantSimulatorClient.Simulations.Protos.IntRange;

namespace PlantSimulatorClient.Simulations.Services
{
    internal static class SimulationServerRequestExtensions
    {
        public static PlantSimulationOptions ToSimulationOptions(this SimulationConfiguration configuration)
        {
            return new PlantSimulationOptions
            {
                Id = configuration.Id,
                Simulation = ToSimulationOptions(configuration.Simulation),
                Environment = ToSimulationEnvironmentOptions(configuration.Environment),
                Plant = ToSimulationPlantOptions(configuration.Plant)
            };
        }

        private static SimulationOptions ToSimulationOptions(GrpcSimulationOptions options)
        {
            return new SimulationOptions
            {
                RandomSeed = options.RandomSeed,
                TickTime = options.TickTime,
                TickEventTime = options.TickEventTime
            };
        }

        private static SimulationPlantOptions ToSimulationPlantOptions(GrpcSimulationPlantOptions options)
        {
            return new SimulationPlantOptions
            {
                SubBranches = ConvertRange(options.SubBranches),
                Axil = options.Axil,
                MaxInternodeLength = ConvertRange(options.InternodeLength),
                PetioleLength = ConvertRange(options.PetioleLength),
                Branches = ConvertRange(options.Branches)
            };
        }

        private static SimulationEnvironmentOptions ToSimulationEnvironmentOptions(
            GrpcSimulationEnvironmentOptions options)
        {
            return new SimulationEnvironmentOptions
            {
                LightSource = new Vertex(options.LightSource.X, options.LightSource.Y, 0),
                Temperature = options.Temperature
            };
        }

        private static Range<int> ConvertRange(IntRange range)
        {
            return new Range<int>(range.Min, range.Max);
        }
    }
}