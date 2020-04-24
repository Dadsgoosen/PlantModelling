using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class SclerenchymaCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Sclerenchyma;

        public SclerenchymaCell()
        {
        }

        public SclerenchymaCell(ICellGeometry geometry, IPlantCell[] neighboringCells, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Neighbors = neighboringCells;
            Vacuole = vacuole;
            CellWall = cellWall;
        }
    }
}