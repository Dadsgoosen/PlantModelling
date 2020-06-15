using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericPetiole : Petiole
    {
        public GenericPetiole(IEnumerable<IPlantCell> cells) : base(cells)
        {
        }

        public GenericPetiole(IEnumerable<IPlantCell> cells, IEnumerable<Leaf> leafs) : base(cells, leafs)
        {
        }
    }
}