using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class CollenchymaCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Collenchyma;

        public CollenchymaCell()
        {
        }

        public CollenchymaCell(ICellGeometry geometry, IVacuole vacuole, ICellWall cellWall) : base(geometry, cellWall, vacuole)
        {
        }
    }
}