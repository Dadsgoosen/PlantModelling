using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Operations
{
    public interface ICellSizer
    {
        public void Resize(IPlantCell a, IPlantCell b);
    }
}