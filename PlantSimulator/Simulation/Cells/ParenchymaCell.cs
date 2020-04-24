using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public class ParenchymaCell : PlantCell
    {
        public override PlantCellType CellType => PlantCellType.Parenchyma;

        public ParenchymaCell()
        {
        }

        public ParenchymaCell(ICellGeometry geometry, IPlantCell[] neighboringCells, IVacuole vacuole, ICellWall cellWall)
        {
            Geometry = geometry;
            Neighbors = neighboringCells;
            Vacuole = vacuole;
            CellWall = cellWall;
        }
    }
}