using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Root : PlantPart
    {
        public override PlantPartType PartType { get; }

        protected Root(IEnumerable<IPlantCell> cells, IEnumerable<Root> connectedRoots, int branchCount) : base(cells, connectedRoots, branchCount)
        {
            PartType = PlantPartType.Root;
        }

        public virtual void ConnectRoot(Root root)
        {
            Connections = new[] {root};
        }
    }
}