using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Runtime
{
    public interface IRuntimeBroker
    {
        public RuntimeStatus Status { get; }

        public Task StartSimulationAsync(SimulationOptions options, CancellationToken cancellationToken);

        public Task StopSimulationAsync(CancellationToken cancellationToken = default);
    }
}