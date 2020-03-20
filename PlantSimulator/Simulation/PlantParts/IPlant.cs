using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    public interface IPlant
    {
        public IShootSystem ShootSystem { get; }

        public IRootSystem RootSystem { get; }
    }
}