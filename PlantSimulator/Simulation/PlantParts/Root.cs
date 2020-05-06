using System.Collections.Generic;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Root : PlantPart
    {
        public IEnumerable<Root> ConnectedRoots { get; }

        public abstract void ConnectRoot(Root root);
    }
}