using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;

namespace PlantSimulator.Simulation.Plants.Fluids
{
    public class FluidsPlant : IPlant
        {
        public IShootSystem ShootSystem { get; }

        public IRootSystem RootSystem { get; }

        public FluidsPlant(GenericShootSystem stem, GenericRootSystem root)
        {
            ShootSystem = stem;
            RootSystem = root;
        }
    }
}