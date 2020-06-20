using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Stem : PlantPart
    {
        public Internode Internode { get; }

        public override PlantPartType PartType { get; }

        protected Stem(Internode internode, int branchCount) : base(new IPlantCell[0], new [] { internode }, branchCount)
        {
            PartType = PlantPartType.Stem;
            Internode = internode;
        }
    }
}