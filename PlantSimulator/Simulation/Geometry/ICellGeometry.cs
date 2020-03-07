using System.Collections.Generic;

namespace PlantSimulator.Simulation.Geometry
{
    public interface ICellGeometry
    {
        public IList<IVertex> Vertices { get; }

        public IVertex CellCenter { get; }
    }
}