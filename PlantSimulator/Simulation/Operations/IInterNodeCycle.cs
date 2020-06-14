using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public interface IInterNodeCycle
    {
        public void Cycle(Internode internode, SimulationStateSnapshot stateSnapshot, float height);
    }
}