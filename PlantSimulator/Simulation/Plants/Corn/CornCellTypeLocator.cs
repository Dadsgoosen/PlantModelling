using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Plants.Corn
{
    public static class CornCellTypeLocator
    {
        public static ICellTypeLocator GetCornCellTypeLocator()
        {
            ICellTypeLocator locator = new CellTypeLocator();

            // left center
            locator.Add(PlantCellType.Xylem, 0, -3);
            locator.Add(PlantCellType.Phloem, -1, -3);

            // right center
            locator.Add(PlantCellType.Xylem, 0, 3);
            locator.Add(PlantCellType.Phloem, -1, 3);

            // top center
            locator.Add(PlantCellType.Phloem, 6, 0);
            locator.Add(PlantCellType.Xylem, 7, 0);

            // bottom center
            locator.Add(PlantCellType.Phloem, -6, 0);
            locator.Add(PlantCellType.Xylem, -7, 0);

            // left middle top
            locator.Add(PlantCellType.Phloem, 4, -4);
            locator.Add(PlantCellType.Xylem, 5, -4);

            // left middle bottom
            locator.Add(PlantCellType.Phloem, -4, -4);
            locator.Add(PlantCellType.Xylem, -5, -4);

            // right middle top
            locator.Add(PlantCellType.Phloem, 4, 4);
            locator.Add(PlantCellType.Xylem, 5, 4);

            // right middle bottom
            locator.Add(PlantCellType.Phloem, -4, 4);
            locator.Add(PlantCellType.Xylem, -5, 4);

            // right edges
            locator.Add(PlantCellType.Xylem, 2, 8);
            locator.Add(PlantCellType.Phloem, 2, 9);

            locator.Add(PlantCellType.Xylem, -2, 8);
            locator.Add(PlantCellType.Phloem, -3, 9);

            // left edges
            locator.Add(PlantCellType.Xylem, 2, -8);
            locator.Add(PlantCellType.Phloem, 2, -9);

            locator.Add(PlantCellType.Xylem, -2, -8);
            locator.Add(PlantCellType.Phloem, -3, -9);

            return locator;
        }
    }
}