namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Stem : PlantPart
    {
        public int BranchCount { get; }

        public Internode Internode { get; }

        protected Stem(Internode internode, int branchCount)
        {
            BranchCount = branchCount;
            Internode = internode;
        }
    }
}