using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;

namespace PlantSimulator.Simulation.Plants.Corn
{
    public class CornPlant : IPlant
    {
        public IShootSystem ShootSystem { get; }

        public IRootSystem RootSystem { get; }

        public CornPlant(GenericShootSystem stem, GenericRootSystem root)
        {
            ShootSystem = stem;
            RootSystem = root;
        }
    }
}