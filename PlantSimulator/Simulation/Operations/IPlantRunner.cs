using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public interface IPlantRunner
    {
        public IPlant Plant { get; }

        public SimulationEnvironment Environment { get; }

        public void Tick(SimulationStateSnapshot stateSnapshot);
    }
}