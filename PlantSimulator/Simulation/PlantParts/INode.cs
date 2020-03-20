using System.Collections.Generic;

namespace PlantSimulator.Simulation.PlantParts
{
    public interface INode : IPlantPart
    {
        /// <summary>
        /// Get all the connected petioles
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IEnumerable<IPetiole> Petioles { get; }

        /// <summary>
        /// Get the upper connected internode
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IInternode UpperInternode { get; set; }

        /// <summary>
        /// Get the lower connected internode
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IInternode LowerInternode { get; }

        /// <summary>
        /// Checks whether the Node has a petiole connected
        /// </summary>
        /// <returns>True if this node has petiole connected, false if not</returns>
        /// <remarks>
        /// This implementation checks whether the <see cref="Petioles"/> is null,
        /// but does not check whether the <see cref="IEnumerable{T}"/> is empty.
        /// </remarks>
        public bool HasPetiole()
        {
            return Petioles != null;
        }

        /// <summary>
        /// Get whether this node has an internode connected to its top
        /// </summary>
        /// <returns></returns>
        public bool HasUpperInternode()
        {
            return UpperInternode != null;
        }

        /// <summary>
        /// Get whether this node has an internode to its bottom.
        /// This should generally always be the case, unless it is the first internode.
        /// </summary>
        /// <returns>True if it has a lower internode connected, false if not</returns>
        public bool HasLowerInternode()
        {
            return LowerInternode != null;
        }
    }
}