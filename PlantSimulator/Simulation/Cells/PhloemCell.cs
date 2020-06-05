using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class PhloemCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Phloem;

        public PhloemCell()
        {
        }

        public PhloemCell(ICellGeometry geometry, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Vacuole = vacuole;
            CellWall = cellWall;
        }

    }
}