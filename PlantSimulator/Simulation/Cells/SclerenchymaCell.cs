using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class SclerenchymaCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Sclerenchyma;

        public SclerenchymaCell()
        {
        }

        public SclerenchymaCell(ICellGeometry geometry, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Vacuole = vacuole;
            CellWall = cellWall;
        }
    }
}