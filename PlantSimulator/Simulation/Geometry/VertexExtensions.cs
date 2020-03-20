using System;

namespace PlantSimulator.Simulation.Geometry
{
    public static class VertexExtensions
    {
        /// <summary>
        /// Compute the length between two <see cref="IVertex"/>
        /// </summary>
        /// <param name="vertex">The vertex to calculate the length from</param>
        /// <param name="to">The vertex to calculate the length to</param>
        /// <returns>The length between the two vertices</returns>
        public static float Length(this IVertex vertex, IVertex to)
        {
            double x = Math.Pow(vertex.X - to.X, 2);
            double y = Math.Pow(vertex.Y - to.Y, 2);
            double z = Math.Pow(vertex.Z - to.Z, 2);
            return (float)Math.Sqrt(x + y + z);
        }
    }
}