using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class PhloemCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Phloem;

        public PhloemCell()
        {
        }

        public PhloemCell(ICellGeometry geometry, IPlantCell[] neighboringCells, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Neighbors = neighboringCells;
            Vacuole = vacuole;
            CellWall = cellWall;
        }

    }
}