using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class ParenchymaCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Parenchyma;

        public ParenchymaCell()
        {
        }

        public ParenchymaCell(ICellGeometry geometry, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Vacuole = vacuole;
            CellWall = cellWall;
        }
    }
}