using System.Threading.Tasks;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Runner
{
    public interface IPlantRunner
    {
        public IPlant Plant { get; }

        public void Tick(SimulationStateSnapshot state);
    }
}