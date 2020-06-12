using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface ICellTypeLocator
    {
        public bool Has(int r, int c);
        public PlantCellType Get(int r, int c);
        public void Add(PlantCellType type, int r, int c);
    }
}