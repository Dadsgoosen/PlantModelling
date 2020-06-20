using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericNode : Node
    {
        public GenericNode(Internode lowerInternode, IEnumerable<IPlantCell> cells, int branchCount) : base(lowerInternode, cells, new Stem[0], new Petiole[0], branchCount)
        {
        }

        public GenericNode(Internode lowerInternode, IEnumerable<IPlantCell> cells, IEnumerable<Stem> stems, int branchCount) : base(lowerInternode, cells, stems, branchCount)
        {
        }

        public GenericNode(Internode lowerInternode, IEnumerable<IPlantCell> cells, IEnumerable<Petiole> petioles, int branchCount) : base(lowerInternode, cells, petioles, branchCount)
        {
        }

        public GenericNode(Internode lowerInternode, IEnumerable<IPlantCell> cells, IEnumerable<Stem> stems, IEnumerable<Petiole> petioles, int branchCount) : base(
            lowerInternode, cells, stems ?? new Stem[0], petioles ?? new Petiole[0], branchCount)
        {
        }
    }
}