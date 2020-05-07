using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Simulations
{
    public interface ISimulationStorage
    {
        public Task StoreSimulationAsync(SimulationState state, CancellationToken cancellationToken = default);

        public Task<IDictionary<string, SimulationState>> GetSimulationState(string id, CancellationToken cancellationToken = default);

        public Task<SimulationState[]> GetSimulationStates(CancellationToken cancellationToken = default);

        public bool DeleteSimulationState(string id);
    }
}