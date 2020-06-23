using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PlantSimulator.Simulation.Cells.Helpers
{
    public static class CellIterator
    {
        public static void IterateCells(IEnumerable<IPlantCell> cells, Action<IPlantCell> action)
        {
            Parallel.ForEach(cells, cell =>
            {
                lock (cell.Synchronizer)
                {
                    action(cell);
                }
            });
        }

    }
}