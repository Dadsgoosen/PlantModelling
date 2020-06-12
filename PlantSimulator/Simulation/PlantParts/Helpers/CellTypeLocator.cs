using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    internal class CellTypeLocator : ICellTypeLocator
    {
        private readonly Dictionary<int, IDictionary<int, PlantCellType>> typeGrid;

        public CellTypeLocator()
        {
            typeGrid = new Dictionary<int, IDictionary<int, PlantCellType>>();
        }

        public bool Has(int r, int c)
        {
            if (typeGrid.TryGetValue(r, out var g))
            {
                if (g.ContainsKey(c))
                {
                    return true;
                }
            }

            return false;
        }

        public PlantCellType Get(int r, int c)
        {
            if (typeGrid.TryGetValue(r, out var g))
            {
                if (g.TryGetValue(c, out var t))
                {
                    return t;
                }
            }

            return PlantCellType.Parenchyma;
        }

        public void Add(PlantCellType type, int r, int c)
        {
            if (typeGrid.TryGetValue(r, out var g))
            {
                g[c] = type;
            }
            else
            {
                typeGrid[r] = new Dictionary<int, PlantCellType>(new[] {KeyValuePair.Create(c, type)});
            }
        }
    }
}