namespace PlantSimulator.Simulation.Options
{
    public class SimulationOptions
    {
        public string Id { get; set; }
        public uint TickTime { get; set; }
        public SimulationPlantOptions Plant { get; set; }
        public SimulationEnvironmentOptions Environment { get; set; }
    }
}