using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public abstract class PlantCell : IPlantCell
    {
        public object Synchronizer { get; } = new object();

        public abstract PlantCellType CellType { get; }
        
        public ICellGeometry Geometry { get; protected set; }
        
        public ICellWall CellWall { get; protected set; }
        
        public IVacuole Vacuole { get; protected set; }

        public virtual float Turgidity
        {
            get
            {
                var turgidity = CellWall.Rigidness * Vacuole.TurgorPressure;
                return turgidity > 1 ? 1 : turgidity;
            }
        }

        public bool IsDead { get; private set; }

        public void Kill()
        {
            IsDead = true;
        }
    }
}