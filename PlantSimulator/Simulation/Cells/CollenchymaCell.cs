using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class CollenchymaCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Collenchyma;

        public CollenchymaCell()
        {
        }

        public CollenchymaCell(ICellGeometry geometry, IPlantCell[] neighboringCells, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Neighbors = neighboringCells;
            Vacuole = vacuole;
            CellWall = cellWall;
        }
    }
}