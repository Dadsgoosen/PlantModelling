using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class PlantPart : IPlantPart
    {
        public IEnumerable<IPlantCell> Cells { get; protected set; }
        
        public IEnumerable<IPlantPart> Connections { get; protected set; }

        protected PlantPart() { }

        protected PlantPart(IEnumerable<IPlantCell> cells, IEnumerable<IPlantPart> connections)
        {
            Cells = cells;
            Connections = connections;
        }

        public virtual void Grow()
        {
            throw new System.NotImplementedException();
        }
    }
}