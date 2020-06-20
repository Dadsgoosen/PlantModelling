using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Node : PlantPart
    {
        public override PlantPartType PartType { get; }
        
        /// <summary>
        /// Backing field for <see cref="Petioles"/>
        /// </summary>
        private List<Petiole> petioles = new List<Petiole>();

        /// <summary>
        /// Get all the connected petioles
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IEnumerable<Petiole> Petioles
        {
            get => petioles;
            set
            {
                petioles = new List<Petiole>(value);
                Connections = value;
            }
        }

        /// <summary>
        /// Backing field for <see cref="Stems"/>
        /// </summary>
        private List<Stem> stems = new List<Stem>();

        /// <summary>
        /// Get all the connected stems
        /// </summary>
        /// <remarks>Can be null</remarks>
        public IEnumerable<Stem> Stems
        {
            get => stems;
            set
            {
                stems = new List<Stem>(value);
                Connections = value;
            }
        }

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

        protected Node() : this(null, new IPlantCell[0], new Stem[0], new Petiole[0], 0)
        {
        }

        protected Node(Internode lowerInternode, IEnumerable<IPlantCell> cells, IEnumerable<Petiole> petioles, int branchCount) :  this(lowerInternode, cells, new Stem[0], petioles, branchCount)
        {
        }

        protected Node(Internode lowerInternode, IEnumerable<IPlantCell> cells, IEnumerable<Stem> stems, int branchCount) : this(lowerInternode, cells, stems, new Petiole[0], branchCount)
        {
        }

        protected Node(Internode lowerInternode, IEnumerable<IPlantCell> cells, IEnumerable<Stem> stems, IEnumerable<Petiole> petioles, int branchCount) : base(cells, branchCount)
        {
            PartType = PlantPartType.Node;
            LowerInternode = lowerInternode;
            Petioles = petioles;
            Stems = stems;
            Connections = UnionConnections(stems, petioles);
        }

        /// <summary>
        /// Helper method to union enumerable <see cref="a"/> with enumerable <see cref="b"/>
        /// </summary>
        /// <param name="a">First array</param>
        /// <param name="b">Second array</param>
        /// <returns>The enumerable union</returns>
        private static IEnumerable<IPlantPart> UnionConnections(IEnumerable<IPlantPart> a, IEnumerable<IPlantPart> b)
        {
            IPlantPart[] aa = (IPlantPart[]) a ?? a.ToArray();
            IPlantPart[] ba = (IPlantPart[]) b ?? b.ToArray();
            return aa.Union(ba);
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