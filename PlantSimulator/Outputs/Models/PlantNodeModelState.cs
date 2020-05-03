using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Outputs.Models
{
    /// <summary>
    /// Data Access Object for a 2D point to describe the plant.
    /// </summary>
    public class PlantNodeModelState
    {
        /// <summary>
        /// Coordinates that defines this <see cref="PlantModelState"/>
        /// </summary>
        public Vector3[] Coordinates { get; set; }
        
        /// <summary>
        /// Thickness of the plant at the (<see cref="X"/>, <see cref="Y"/>)
        /// </summary>
        public int Thickness { get; set; }

        /// <summary>
        /// Branch connections to this node
        /// </summary>
        public IEnumerable<PlantNodeModelState> Connections { get; set; }
    }
}