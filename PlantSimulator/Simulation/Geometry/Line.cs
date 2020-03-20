using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace PlantSimulator.Simulation.Geometry
{
    public class Line : IEdge
    {
        public IVertex[] Points { get; }

        public Line(IVertex[] vertices)
        {
            Points = vertices;
        }

        public Line(IEnumerable<IVertex> vertices)
        {
            Points = (IVertex[]) vertices ?? vertices.ToArray();
        }
    }
}