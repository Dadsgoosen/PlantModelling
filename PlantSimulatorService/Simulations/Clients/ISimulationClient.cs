using System.Threading.Tasks;
using PlantSimulatorService.Simulations.Model;
using PlantSimulatorService.Simulations.Model.Options;

namespace PlantSimulatorService.Simulations.Clients
{
    public interface ISimulationClient
    {
        public string Id { get; }

        public bool Available { get; }

        public string Host { get; }

        public Task StartAsync(PlantSimulationOptions options);

        public Task StopAsync();
    }
}