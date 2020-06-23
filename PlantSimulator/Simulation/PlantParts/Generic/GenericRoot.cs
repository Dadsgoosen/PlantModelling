using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericRoot : Root
    {
        public GenericRoot(IEnumerable<IPlantCell> cells, IEnumerable<Root> connections, int branchCount) : base(cells, connections, branchCount)
        {
        }

        public GenericRoot(IEnumerable<IPlantCell> cells, int branchCount) : this(cells, new Root[0],  branchCount)
        {
        }

    }
}