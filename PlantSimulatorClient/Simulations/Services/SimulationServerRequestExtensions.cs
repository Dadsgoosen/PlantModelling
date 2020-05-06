using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Options;
using PlantSimulatorClient.Simulations.Protos;

namespace PlantSimulatorClient.Simulations.Services
{
    internal static class SimulationServerRequestExtensions
    {
        public static PlantSimulationOptions ToSimulationOptions(this SimulationConfiguration configuration)
        {
            return new PlantSimulationOptions
            {
                Id = configuration.Id,
                TickTime = configuration.TickTime
            };
        }
    }
}