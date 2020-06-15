namespace PlantSimulator.Simulation.Options
{
    /// <summary>
    /// Interface for all data holder object service for the current active <see cref="IPlantSimulatorOptions"/>
    /// so that other objects can request and use the newest options without it being
    /// passed down through method parameters or likewise.
    /// </summary>
    public interface IPlantSimulatorOptionsService
    {
        /// <summary>
        /// Get the current options set by the plant simulator runtime.
        /// </summary>
        public IPlantSimulatorOptions Options { get; set; }
    }
}