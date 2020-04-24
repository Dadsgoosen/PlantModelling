using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public abstract class PlantCell : IPlantCell
    {
        public abstract PlantCellType CellType { get; }
        
        public ICellGeometry Geometry { get; protected set; }
        
        public IPlantCell[] Neighbors { get; protected set; }
        
        public ICellWall CellWall { get; protected set; }
        
        public IVacuole Vacuole { get; protected set; }

        public virtual float TurgorPressure => 0F;

        public bool IsDead { get; private set; }

        public void Kill()
        {
            IsDead = true;
        }
    }
}