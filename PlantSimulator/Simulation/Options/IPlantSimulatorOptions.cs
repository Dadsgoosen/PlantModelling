namespace PlantSimulator.Simulation.Options
{
    public interface IPlantSimulatorOptions
    {
        public string Id { get; }

        public ISimulationOptions Simulation { get; }

        public SimulationPlantOptions Plant { get; }

        public SimulationEnvironmentOptions Environment { get; }
    }
}