using System.Threading;
using System.Threading.Tasks;
using PlantSimulatorService.Simulations.Clients;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Context
{
    public interface ISimulationClientContext
    {
        public ISimulationClient SayHello(string ip);
        public void SayGoodbye(string id);
        public Task TransmitState(SimulationState state, CancellationToken cancellationToken = default);
    }
}