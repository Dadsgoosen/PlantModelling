using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace PlantSimulator.Simulation.Geometry
{
    public class Face : IFace
    {
        public IVertex[] Points { get; }

        public Face(IVertex[] vertices)
        {
            Points = vertices;
        }

        public Face(IEnumerable<IVertex> vertices)
        {
            Points = (IVertex[]) vertices ?? vertices.ToArray();
        }

        public bool Equals(IFace face)
        {
            for (int i = 0; i < Points.Length; i++)
            {
                if (!face.Points[i].Equals(Points[i])) return false;
            }

            return true;
        }
    }
}