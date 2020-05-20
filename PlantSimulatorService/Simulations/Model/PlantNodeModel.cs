using System.Collections.Generic;
using System.Numerics;
using PlantSimulatorService.Simulations.Protos;

namespace PlantSimulatorService.Simulations.Model
{
    /// <summary>
    /// Data Access Object for a plant part.
    /// </summary>
    public class PlantNodeModel
    {
        /// <summary>
        /// Coordinates that defines this <see cref="PlantModelState"/>
        /// </summary>
        public Vector2[] Coordinates { get; set; }

        /// <summary>
        /// Thickness of the plant at the (<see cref="X"/>, <see cref="Y"/>)
        /// </summary>
        public int Thickness { get; set; }

        /// <summary>
        /// Branch connections to this node
        /// </summary>
        public IEnumerable<PlantNodeModel> Connections { get; set; }
    }
}