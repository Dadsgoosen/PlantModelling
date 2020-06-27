using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlantSimulator.Simulation.Cells.Helpers
{
    public static class CellIterator
    {
        public static void IterateCells(IEnumerable<IPlantCell> cells, Action<IPlantCell> action)
        {
            foreach (var cell in cells)
            {
                action(cell);
            }
        }

    }
}