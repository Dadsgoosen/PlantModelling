using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public interface ICellBodySystemSolver
    {
        public void Solve(IPlantPart part);
    }
}