namespace PlantSimulatorService.Simulations.Model.Options
{
    public interface IPlantSimulatorOptions
    {
        public string Id { get; }

        public ISimulationOptions Simulation { get; }

        public ISimulationPlantOptions Plant { get; }

        public ISimulationEnvironmentOptions Environment { get; }
    }
}