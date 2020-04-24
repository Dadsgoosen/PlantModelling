using System.Threading.Tasks;
using PlantSimulator.Outputs.Models;

namespace PlantSimulator.Outputs
{
    public interface IStateTransmitter
    {
        public Task TransmitStateAsync(SimulationState state);
    }
}