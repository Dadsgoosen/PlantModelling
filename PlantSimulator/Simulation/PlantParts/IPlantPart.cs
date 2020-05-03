using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.PlantParts
{
    /// <summary>
    /// Interface for all the things that the different plant parts have in common.
    /// This includes plant cells, the ability to grow etc.
    /// </summary>
    public interface IPlantPart
    {
        /// <summary>
        /// Get all the plant cells
        /// </summary>
        public IEnumerable<IPlantCell> Cells { get; }

        /// <summary>
        /// Get the <see cref="IPlantPart"/> that this <see cref="IPlantPart"/> is connected to
        /// </summary>
        public IEnumerable<IPlantPart> Connections { get; }

        /// <summary>
        /// Let this plant part grow
        /// </summary>
        public void Grow();
    }
}