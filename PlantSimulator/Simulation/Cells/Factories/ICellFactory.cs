using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells.Factories
{
    public interface ICellFactory
    {
        public IPlantCell CreateCell(PlantCellType type, ICellGeometry geometry, IVacuole vacuole, ICellWall wall);
    }
}