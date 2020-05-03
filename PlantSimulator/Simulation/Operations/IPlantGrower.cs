using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public interface IPlantGrower
    {
        public void GrowPlant(IPlant plant, SimulationStateSnapshot state);
    }
}