using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Runtime
{
    public interface IRuntimeBroker<out TRuntime>
    {
        public TRuntime Simulation { get; }

        public RuntimeStatus Status { get; }

        public Task StartSimulationAsync(PlantSimulationOptions options, CancellationToken cancellationToken);

        public Task StopSimulationAsync(CancellationToken cancellationToken = default);
    }
}