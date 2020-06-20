using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class PlantPartCellCreator : IPlantPartCellCreator
    {
        private readonly ICellGridFactory cellGridFactory;

        public PlantPartCellCreator(ICellGridFactory cellGridFactory)
        {
            this.cellGridFactory = cellGridFactory;
        }

        public IList<IPlantCell> CreateCells(float width, float depth, Vector3 center, float height)
        {
            return new List<IPlantCell>(cellGridFactory.CreateCellGrid(width, depth, center, height));
        }

    }
}