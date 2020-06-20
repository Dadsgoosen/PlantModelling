using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface IPlantPartCellCreator
    {
        public IList<IPlantCell> CreateCells(float width, float depth, Vector3 center, float height);
    }
}