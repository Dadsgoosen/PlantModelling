using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Internode : PlantPart
    {
        public override PlantPartType PartType { get; }

        private Node upperNode;

        public Node UpperNode
        {
            get => upperNode;
            set
            {
                upperNode = value;
                Connections = new[] {upperNode};
            }
        }

        public Node LowerNode { get; set; }

        protected Internode()
        {
            PartType = PlantPartType.Internode;
            Connections = new IPlantPart[0];
        }

        protected Internode(IEnumerable<IPlantCell> cells) : base(cells, new IPlantPart[0])
        {
            PartType = PlantPartType.Internode;
        }

        protected Internode(IEnumerable<IPlantCell> cells, IEnumerable<IPlantPart> connections) : base(cells, connections)
        {
            PartType = PlantPartType.Internode;
        }

        public bool HasUpperNode()
        {
            return UpperNode != null;
        }

        public bool HasLowerNode()
        {
            return LowerNode != null;
        }
    }
}