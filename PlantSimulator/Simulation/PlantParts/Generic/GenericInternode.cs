using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericInternode : Internode
    {
        public GenericInternode(IEnumerable<IPlantCell> cells, Node lowerNode) : base(cells)
        {
            LowerNode = lowerNode;
        }

    }
}