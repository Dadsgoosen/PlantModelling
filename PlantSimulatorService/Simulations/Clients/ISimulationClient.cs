using System.Threading.Tasks;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Simulations.Clients
{
    public interface ISimulationClient
    {
        public string Id { get; }

        public bool Available { get; }

        public string Host { get; }

        public Task StartAsync(SimulationOptions options);

        public Task StopAsync();
    }
}