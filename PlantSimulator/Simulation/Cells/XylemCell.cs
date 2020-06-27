using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class XylemCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Xylem;

        public XylemCell()
        {
            Kill();
        }

        public XylemCell(ICellGeometry geometry, IVacuole vacuole, ICellWall cellWall) : this()
        {
            Geometry = geometry;
            Vacuole = vacuole;
            CellWall = cellWall;
        }
    }
}