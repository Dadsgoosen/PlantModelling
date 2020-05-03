using System.Collections.Generic;

namespace PlantSimulator.Simulation.PlantParts
{
    public interface IRoot
    {
        public IEnumerable<IRoot> ConnectedRoots { get; }

        public void ConnectRoot(IRoot root);
    }
}