using System.Collections.Generic;

namespace PlantSimulator.Simulation.Geometry
{
    /// <summary>
    /// Interface for representing a point in a 3-dimensional space.
    /// </summary>
    public interface IVertex
    {
        /// <summary>
        /// The x coordinate
        /// </summary>
        public float X { get; set; }

        /// <summary>
        /// The y coordinate
        /// </summary>
        public float Y { get; set; }

        /// <summary>
        /// The z coordinate
        /// </summary>
        public float Z { get; set; }

        /// <summary>
        /// Move this vertex to a new location.
        /// Both the <see cref="X"/>, <see cref="Y"/> and <see cref="Z"/> will be overwritten by the new <see cref="location"/>.
        /// </summary>
        /// <param name="location">The new location.</param>
        void MoveTo(IVertex location);
    }
}