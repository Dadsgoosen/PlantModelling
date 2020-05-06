using System.Threading.Tasks;
using PlantSimulator.Simulation;

namespace PlantSimulator.Runtime
{
    public interface ISimulatorEventHandler
    {
        public void OnSimulationTick(object? sender, PlantSimulatorTickEvent tickEvent);
    }
}