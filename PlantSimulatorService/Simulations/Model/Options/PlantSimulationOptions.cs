namespace PlantSimulatorService.Simulations.Model.Options
{
    public class PlantSimulationOptions
    {
        public string Id { get; set; }
        
        public SimulationOptions Simulation { get; set; }

        public SimulationPlantOptions Plant { get; set; }

        public SimulationEnvironmentOptions Environment { get; set; }
    }
}