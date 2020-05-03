namespace PlantSimulator.Simulation.Options
{
    public class SimulationPlantOptions
    {
        public int[] Branches { get; set; }

        public float InternodeLength { get; set; }

        public SimulationBranchingOptions Branching { get; set; }
    }
}