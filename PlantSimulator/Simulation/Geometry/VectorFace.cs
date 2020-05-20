using System;
using System.Numerics;

namespace PlantSimulator.Simulation.Geometry
{
    public struct VectorFace
    {
        public Vector3[] Points;

        public VectorFace(ref Vector3[] points)
        {
            Points = points;
        }
    }
}