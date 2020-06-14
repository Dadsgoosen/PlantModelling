namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Stem : PlantPart
    {
        public int BranchCount { get; }

        public Internode Internode { get; }

        public override PlantPartType PartType { get; }

        protected Stem(Internode internode, int branchCount)
        {
            PartType = PlantPartType.Stem;
            BranchCount = branchCount;
            Internode = internode;
        }
    }
}