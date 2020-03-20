using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Operations
{
    public interface ICellDivider
    {
        bool ShouldDivide(IPlantCell cell);
        IPlantCell[] Divide(IPlantCell cell);
    }
}