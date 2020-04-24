using PlantSimulator.Simulation;
using PlantSimulatorClient.Simulations.Protos;

namespace PlantSimulatorClient.Simulations.Services
{
    internal static class SimulationServerRequestExtensions
    {
        public static SimulationOptions ToSimulationOptions(this SimulationConfiguration configuration)
        {
            return new SimulationOptions
            {
                Id = configuration.Id,
                TickTime = configuration.TickTime
            };
        }
    }
}