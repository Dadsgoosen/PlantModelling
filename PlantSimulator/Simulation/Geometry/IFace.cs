using System.Collections.Generic;

namespace PlantSimulator.Simulation.Geometry
{
    public interface IFace
    {
        public IVertex[] Points { get; }
    }
}