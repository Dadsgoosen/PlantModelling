using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericRoot : Root
    {
        private readonly IList<Root> connectedRoots;

        public new IEnumerable<Root> ConnectedRoots => connectedRoots;
        
        public GenericRoot(IEnumerable<IPlantCell> cells, IEnumerable<Root> connections)
        {
            Cells = new List<IPlantCell>(cells);
            connectedRoots = new List<Root>(connections);
            Connections = connectedRoots;
        }
        public GenericRoot(IEnumerable<IPlantCell> cells)
        {
            Cells = new List<IPlantCell>(cells);
            connectedRoots = new List<Root>();
            Connections = connectedRoots;
        }

        public override void ConnectRoot(Root root)
        {
            connectedRoots.Add(root);
        }
    }
}