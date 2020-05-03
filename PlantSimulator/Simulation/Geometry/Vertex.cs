using System;

namespace PlantSimulator.Simulation.Geometry
{
    public class Vertex : IVertex
    {
        public float X { get; set; }
        public float Y { get; set; }
        public float Z { get; set; }

        public Vertex() { }

        public Vertex(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public bool Equals(IVertex obj)
        {
            var tolerance = 0.0001F;
            return Math.Abs(X - obj.X) < tolerance && Math.Abs(Y - obj.Y) < tolerance && Math.Abs(Z - obj.Z) < tolerance;
        }

        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}