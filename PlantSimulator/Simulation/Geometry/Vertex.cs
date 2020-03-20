using System;
using System.Collections.Generic;

namespace PlantSimulator.Simulation.Geometry
{
    /// <summary>
    /// A <see cref="Vertex"/> represents a single point in a 3-dimensional space, and can be connected to other points
    /// </summary>
    public class Vertex : IVertex
    {
        /// <summary>
        /// Readonly unique id for this vertex
        /// </summary>
        private readonly int id = Guid.NewGuid().GetHashCode();

        /// <summary>
        /// The X coordinate. Also denoted as Width.
        /// </summary>
        public float X { get; set; }
     
        /// <summary>
        /// The Y coordinate. Also denoted as Height.
        /// </summary>
        public float Y { get; set; }
        
        /// <summary>
        /// The Z coordinate. Also denoted as Depth.
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Instantiate a new vertex by giving <see cref="x"/>, <see cref="y"/> and <see cref="z"/> coordinates.
        /// Initiating the Connection list with a default capacity of 3.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="z">The z coordinate</param>
        public Vertex(float x, float y, float z)
        {
            SetCoordinates(x, y, z);
        }

        /// <summary>
        /// Move this point to a new location.
        /// </summary>
        /// <param name="location">The new location</param>
        public void MoveTo(IVertex location)
        {
            SetCoordinates(location.X, location.Y, location.Z);
        }

        /// <summary>
        /// Set the new x, y and z coordinates to new values.
        /// </summary>
        /// <param name="x">The x coordinate</param>
        /// <param name="y">The y coordinate</param>
        /// <param name="z">The z coordinate</param>
        private void SetCoordinates(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public override int GetHashCode()
        {
            return 13 * id;
        }

        /// <summary>
        /// Display the string representation of this vertex point.
        /// </summary>
        /// <returns>(X, Y, Z)</returns>
        public override string ToString()
        {
            return $"({X}, {Y}, {Z})";
        }
    }
}