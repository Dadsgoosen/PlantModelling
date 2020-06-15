using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Development
{
    public interface IPlantPartDevelopment<in T> where T : IPlantPart
    {
        public void Develop(T plantPart, SimulationStateSnapshot snapshot);
    }
}