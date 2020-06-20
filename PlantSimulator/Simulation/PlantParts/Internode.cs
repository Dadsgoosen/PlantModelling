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

        protected Internode() : base(new IPlantCell[0], new IPlantPart[0], 0)
        {
            PartType = PlantPartType.Internode;
        }

        protected Internode(IEnumerable<IPlantCell> cells, int branchCount) : base(cells, branchCount)
        {
            PartType = PlantPartType.Internode;
        }

        protected Internode(IEnumerable<IPlantCell> cells, Node lowerNode, int branchCount) : base(cells, branchCount)
        {
            PartType = PlantPartType.Internode;
            LowerNode = lowerNode;
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