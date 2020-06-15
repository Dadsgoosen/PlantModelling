using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    /// <summary>
    /// Is a stem support 
    /// </summary>
    public abstract class Petiole : PlantPart
    {
        public override PlantPartType PartType { get; }

        public IEnumerable<Leaf> Leafs { get; }

        /// <summary>
        /// The angle between the shoot stem and the petiole, branch or bud
        /// </summary>
        public float Axil { get; }

        /// <summary>
        /// The node that the petiole is connected to
        /// </summary>
        public Node Node { get; }

        protected Petiole(IEnumerable<IPlantCell> cells) : this(cells, new Leaf[0])
        {
            PartType = PlantPartType.Petiole;
        }

        protected Petiole(IEnumerable<IPlantCell> cells, IEnumerable<Leaf> leafs)
        {
            PartType = PlantPartType.Petiole;
            Leafs = leafs;
            Cells = cells;
            Connections = Leafs;
        }
    }
}