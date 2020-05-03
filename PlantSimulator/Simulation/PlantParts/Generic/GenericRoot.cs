using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericRoot : PlantPart, IRoot
    {
        private readonly IList<IRoot> _connectedRoots;

        public IEnumerable<IRoot> ConnectedRoots => _connectedRoots;

        public GenericRoot(IEnumerable<IPlantCell> cells, IEnumerable<IRoot> connections)
        {
            Cells = new List<IPlantCell>(cells);
            _connectedRoots = new List<IRoot>(connections);
            Connections = MapToPlantPart(connections);
        }

        public void ConnectRoot(IRoot root)
        {
        }

        private IEnumerable<IPlantPart> MapToPlantPart(IEnumerable<IRoot> roots)
        {
            IList<IPlantPart> parts = new List<IPlantPart>(roots.Count());

            foreach (var root in roots)
            {
                parts.Add((IPlantPart) root);
            }

            return parts;
        }
    }
}