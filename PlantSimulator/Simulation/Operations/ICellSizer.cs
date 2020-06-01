using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Operations
{
    public interface ICellSizer
    {
        public void ResizeWidth(IPlantCell a, IPlantCell b);
        public void ResizeHeight(IPlantCell a, IPlantCell b);
    }
}