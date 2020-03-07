using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Operations
{
    public interface ICellDivider
    {
        ICell[] Divide(ICell cell);
    }
}