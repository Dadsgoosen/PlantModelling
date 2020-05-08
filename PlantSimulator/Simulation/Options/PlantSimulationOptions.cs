namespace PlantSimulator.Simulation.Options
{
    public class PlantSimulationOptions : IPlantSimulatorOptions
    {
        public string Id { get; set; }
        
        public ISimulationOptions Simulation { get; set; }

        public ISimulationPlantOptions Plant { get; set; }

        public ISimulationEnvironmentOptions Environment { get; set; }
    }
}