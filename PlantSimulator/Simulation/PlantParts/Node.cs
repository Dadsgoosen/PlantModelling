using System.Collections.Generic;
using System.Linq;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Node : PlantPart
    {
        public override PlantPartType PartType { get; }

        /// <summary>
        /// Get all the connected petioles
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IEnumerable<Petiole> Petioles { get; }

        /// <summary>
        /// Get all the connected stems
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IEnumerable<Stem> Stems { get; }

        /// <summary>
        /// Get the upper connected internode
        /// </summary>
        /// <remarks>Can be null</remarks>
        public Internode UpperInternode { get; set; }

        /// <summary>
        /// Get the lower connected internode
        /// </summary>
        /// <remarks>Can be null</remarks>
        public Internode LowerInternode { get; protected set; }

        protected Node()
        {
            PartType = PlantPartType.Node;
        }

        /// <summary>
        /// Checks whether the Node have any petiole connected
        /// </summary>
        /// <returns>True if this node has petiole connected, false if not</returns>
        /// <remarks>
        /// This implementation checks whether the <see cref="Petioles"/> is null.
        /// </remarks>
        public bool HaveConnectedPetiole()
        {
            return Petioles != null || Petioles.Any();
        }

        /// <summary>
        /// Checks whether the Node have any stems connected
        /// </summary>
        /// <returns>True if this node have stems connected, false if not</returns>
        /// <remarks>
        /// This implementation checks whether the <see cref="Petioles"/> is null.
        /// </remarks>
        public bool HaveConnectedStems()
        {
            return Stems != null || Stems.Any();
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