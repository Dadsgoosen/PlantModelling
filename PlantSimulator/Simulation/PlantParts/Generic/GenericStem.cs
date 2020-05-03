using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericStem : PlantPart, IStem
    {
        public IInternode Internode { get; }

        public GenericStem(IEnumerable<IPlantCell> cells)
        {
            Cells = new List<IPlantCell>(cells);
            Connections = new List<IPlantPart>(1);
            Internode = null;
        }

        public GenericStem(IEnumerable<IPlantCell> cells, IInternode internode)
        {
            Cells = new List<IPlantCell>(cells);
            Connections = new[] {(IPlantPart) internode};
            Internode = internode;
        }
    }
}