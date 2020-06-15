using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Development
{
    public interface IPlantPartDeveloper
    {
        public void Develop(IPlantPart plantPart, SimulationStateSnapshot snapshot);
    }
}