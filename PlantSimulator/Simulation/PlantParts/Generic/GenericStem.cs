using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericStem : Stem
    {
        public GenericStem(IEnumerable<IPlantCell> cells, int branchCount) : base(null, branchCount)
        {
            Cells = new List<IPlantCell>(cells);
            Connections = new List<IPlantPart>(1);
        }

        public GenericStem(IEnumerable<IPlantCell> cells, Internode internode, int branchCount) : base(internode, branchCount)
        {
            Cells = new List<IPlantCell>(cells);
            Connections = new[] {internode};
        }
    }
}