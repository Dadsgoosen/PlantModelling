using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class XylemCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Xylem;

        public XylemCell()
        {
        }

        public XylemCell(ICellGeometry geometry, IPlantCell[] neighboringCells, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Neighbors = neighboringCells;
            Vacuole = vacuole;
            CellWall = cellWall;
            Kill();
        }
    }
}