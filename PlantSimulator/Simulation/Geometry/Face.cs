using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;

namespace PlantSimulator.Simulation.Geometry
{
    public class Face : IFace
    {
        public Vector2[] Points { get; }

        public Face(Vector2[] vertices)
        {
            Points = vertices;
        }

        public Face(IEnumerable<Vector2> vertices)
        {
            Points = (Vector2[]) vertices ?? vertices.ToArray();
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