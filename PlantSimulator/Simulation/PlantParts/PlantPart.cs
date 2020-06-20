using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    /// <summary>
    /// Base abstract class for a <see cref="PlantPart"/> that all other plant parts must inherit from.
    /// </summary>
    public abstract class PlantPart : IPlantPart
    {
        public object Synchronizer { get; } = new object();

        /// <summary>
        /// Get the branch counter for which this plant part is part of
        /// </summary>
        public int BranchCount { get; }

        /// <summary>
        /// Get the type of plant part
        /// </summary>
        public abstract PlantPartType PartType { get; }

        /// <summary>
        /// Backing field for <see cref="Cells"/> property
        /// </summary>
        private readonly List<IPlantCell> cells = new List<IPlantCell>();

        /// <summary>
        /// Get an enumerable of all the plant cells for this plant part
        /// </summary>
        public IEnumerable<IPlantCell> Cells
        {
            get => cells;
            set
            {
                if (value == null) return;
                cells.AddRange(value);
            }
        }

        /// <summary>
        /// Field for the <see cref="Connections"/> property
        /// </summary>
        private readonly List<IPlantPart> connections = new List<IPlantPart>();

        /// <summary>
        /// Get an enumerable of all the forward connected plant parts
        /// </summary>
        public IEnumerable<IPlantPart> Connections
        {
            get => connections;
            set
            {
                if (value == null) return;
                connections.AddRange(value);
            }
        }

        /// <summary>
        /// Instantiate an empty <see cref="PlantPart"/>
        /// </summary>
        protected PlantPart() { }

        /// <summary>
        /// Instantiate a new <see cref="PlantPart"/> with the provided plant cells and connections
        /// </summary>
        /// <param name="cells">The plant cells for this plant part</param>
        /// <param name="connections">The <see cref="PlantPart"/> connections for this <see cref="PlantPart"/></param>
        /// <param name="branchCount">The branch counter for which this plant part is part of</param>
        protected PlantPart(IEnumerable<IPlantCell> cells, IEnumerable<IPlantPart> connections, int branchCount)
        {
            Cells = cells;
            Connections = connections;
            BranchCount = branchCount;
        }

        /// <summary>
        /// Instantiate a new <see cref="PlantPart"/> with the provided cells and zero connections
        /// </summary>
        /// <param name="cells">The plant cells for this plant part</param>
        /// <param name="branchCount">The branch counter for which this plant part is part of</param>
        protected PlantPart(IEnumerable<IPlantCell> cells, int branchCount)
        {
            Cells = cells;
            BranchCount = branchCount;
        }
    }
}