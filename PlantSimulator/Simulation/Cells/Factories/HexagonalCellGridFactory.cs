using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Cells.Factories
{
    public class HexagonalCellGridFactory : ICellGridFactory
    {
        private readonly ISingularCellCreator cellCreator;

        private readonly ICellTypeLocator cellTypeLocator;

        private readonly IPlantSimulatorOptionsService optionsService;

        public HexagonalCellGridFactory(ISingularCellCreator cellCreator, ICellTypeLocator cellTypeLocator, IPlantSimulatorOptionsService optionsService)
        {
            this.cellCreator = cellCreator;
            this.cellTypeLocator = cellTypeLocator;
            this.optionsService = optionsService;
        }

        public IEnumerable<IPlantCell> CreateCellGrid(float width, float depth, Vector3 center, float height)
        {
            float hexagonRadius = optionsService.Options.Cell.XylemCellSize;
            float hexagonWidth = 2 * hexagonRadius;
            float hexagonHeight = (float)Math.Sqrt(3) * hexagonRadius;

            int rows = (int) depth;
            int columns = (int) width;

            var cells = new List<IPlantCell>();

            // This variable is used to keep track of how many rows should be created for a specific column.
            int current = rows;

            // The amount on the right side of center
            int right = (int)Math.Ceiling(columns / 2f);
            // The amount on the left side of center
            int left = (int)Math.Floor(columns / 2f);

            // The center and right side
            for (int c = 0; c < right; c++)
            {
                cells.AddRange(CreateEvenOddColumns(current, c, hexagonRadius, hexagonWidth, hexagonHeight, center, height));
                current--;
            }

            // Without the center, so - 1, and then left side
            current = rows - 1;

            for (int c = -1; c >= -left; c--)
            {
                cells.AddRange(CreateEvenOddColumns(current, c, hexagonRadius, hexagonWidth, hexagonHeight, center, height));
                current--;
            }

            return cells;
        }

        private IEnumerable<IPlantCell> CreateEvenOddColumns(int amount, int column, float hexagonRadius, float hexagonWidth, float hexagonHeight, Vector3 center, float height)
        {
            if (column % 2 == 0)
            {
                return CreateEvenColumns(amount, column, hexagonRadius, hexagonWidth, hexagonHeight, center, height);
            }
            return CreateOddColumns(amount, column, hexagonRadius, hexagonWidth, hexagonHeight, center, height);
        }

        private IEnumerable<IPlantCell> CreateOddColumns(int amount, int column, float hexagonRadius, float hexagonWidth, float hexagonHeight, Vector3 center, float height)
        {
            var cells = new List<IPlantCell>(amount);

            int half = (int)Math.Floor(amount / 2m);

            float x = ComputeX(column, hexagonWidth);
            float z = hexagonRadius;

            for (int i = 0; i < half; i++)
            {
                var newCellType = GetCellType(i, column);

                var newCellCenter = new Vector3(x, center.Y, z);

                cells.Add(cellCreator.CreateCell(newCellType, newCellCenter, hexagonRadius, height));

                z += hexagonHeight;
            }

            z = -hexagonRadius;

            for (int i = -1; i >= -half; i--)
            {
                var newCellType = GetCellType(i, column);

                var newCellCenter = new Vector3(x, center.Y, z);

                cells.Add(cellCreator.CreateCell(newCellType, newCellCenter, hexagonRadius, height));

                z -= hexagonHeight;
            }

            return cells;
        }

        private IEnumerable<IPlantCell> CreateEvenColumns(int amount, int column, float hexagonRadius, float hexagonWidth, float hexagonHeight, Vector3 center, float height)
        {
            var cells = new List<IPlantCell>(amount);

            int half = (int)Math.Floor(amount / 2m);

            float x = ComputeX(column, hexagonWidth);
            float z = ComputeZ(0, hexagonHeight);

            var newCellType = GetCellType(0, column);

            var newCellCenter = new Vector3(x, center.Y, z);

            cells.Add(cellCreator.CreateCell(newCellType, newCellCenter, hexagonRadius, height));

            for (int i = 1; i <= half; i++)
            {
                z = ComputeZ(i, hexagonWidth);

                newCellType = GetCellType(i, column);

                newCellCenter = new Vector3(x, center.Y, z);

                cells.Add(cellCreator.CreateCell(newCellType, newCellCenter, hexagonRadius, height));
            }

            for (int i = -1; i >= -half; i--)
            {
                z = ComputeZ(i, hexagonWidth);

                newCellType = GetCellType(i, column);

                newCellCenter = new Vector3(x, center.Y, z);

                cells.Add(cellCreator.CreateCell(newCellType, newCellCenter, hexagonRadius, height));
            }

            return cells;
        }

        private PlantCellType GetCellType(int row, int column)
        {
            return cellTypeLocator.Get(row, column);
        }

        private static float ComputeZ(int row, float height)
        {
            return row * height;
        }

        private static float ComputeX(int column, float width)
        {
            return column * width;
        }
    }
}