using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericInternode : Internode
    {
        public GenericInternode(IEnumerable<IPlantCell> cells, Node lowerNode, int branchCount) : base(cells, lowerNode, branchCount)
        {
            LowerNode = lowerNode;
        }

        public GenericInternode(IEnumerable<IPlantCell> cells, int branchCount) : base(cells, branchCount)
        {
        }
    }
}