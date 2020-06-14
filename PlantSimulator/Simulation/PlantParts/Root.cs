using System.Collections.Generic;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Root : PlantPart
    {
        public override PlantPartType PartType { get; }

        public IEnumerable<Root> ConnectedRoots { get; }

        public abstract void ConnectRoot(Root root);

        protected Root()
        {
            PartType = PlantPartType.Root;
        }
    }
}