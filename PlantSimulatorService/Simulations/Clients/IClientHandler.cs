using System.Threading.Tasks;
using PlantSimulatorService.Simulations.Model;
using PlantSimulatorService.Simulations.Model.Options;

namespace PlantSimulatorService.Simulations.Clients
{
    public interface IClientHandler
    {
        /// <summary>
        /// Start a simulation with an available client
        /// </summary>
        /// <param name="options">The simulation options that the simulation will run with</param>
        public Task StartAvailableClientAsync(PlantSimulationOptions options);
    }
}