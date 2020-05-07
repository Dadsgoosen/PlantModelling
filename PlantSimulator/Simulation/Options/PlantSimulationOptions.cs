namespace PlantSimulator.Simulation.Options
{
    public class PlantSimulationOptions : IPlantSimulatorOptions
    {
        public string Id { get; set; }
        
        public ISimulationOptions Simulation { get; set; }

        public SimulationPlantOptions Plant { get; set; }

        public SimulationEnvironmentOptions Environment { get; set; }
    }
}