using PlantSimulator.Simulation.Cells.Storage;
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

        public StarchStorage StarchStorage { get; }

        public bool IsDead { get; private set; }

        protected PlantCell()
        {
            StarchStorage = new StarchStorage(0);
        }

        protected PlantCell(ICellGeometry geometry, ICellWall cellWall, IVacuole vacuole)
        {
            StarchStorage = new StarchStorage(0);
        }

        public void Kill()
        {
            IsDead = true;
        }
    }
}