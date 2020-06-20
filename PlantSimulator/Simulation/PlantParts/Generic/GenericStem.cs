using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericStem : Stem
    {
        public GenericStem(Internode internode, int branchCount) : base(internode, branchCount)
        {
        }
        public GenericStem(Internode internode) : base(internode, internode.BranchCount)
        {
        }
    }
}