using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface ICellCreatorHelper
    {
        public IList<IPlantCell> CreateCell(int radius);
    }
}