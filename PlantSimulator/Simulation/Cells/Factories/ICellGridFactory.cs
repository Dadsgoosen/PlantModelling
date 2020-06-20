using System.Collections.Generic;
using System.Numerics;

namespace PlantSimulator.Simulation.Cells.Factories
{
    public interface ICellGridFactory
    {
        public IEnumerable<IPlantCell> CreateCellGrid(float width, float depth, Vector3 center, float height);
    }
}