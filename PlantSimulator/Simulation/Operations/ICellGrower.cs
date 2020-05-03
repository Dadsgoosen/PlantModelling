using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public interface ICellGrower
    {
        public void GrowShootCell(IPlantCell cell, IPlantPart plantPart, SimulationStateSnapshot state);
        public void GrowRootCell(IPlantCell cell, IPlantPart plantPart, SimulationStateSnapshot state);
    }
}