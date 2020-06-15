namespace PlantSimulator.Simulation.Options
{
    /// <summary>
    /// Data holder object for the current active <see cref="IPlantSimulatorOptions"/>
    /// so that other objects can request and use the newest options without it being
    /// passed down through method parameters or likewise.
    /// </summary>
    public class PlantSimulatorOptionsService : IPlantSimulatorOptionsService
    {
        /// <summary>
        /// Get the current options set by the plant simulator runtime.
        /// </summary>
        /// <remarks>
        /// Should not be cached in objects as it can be changed between simulation runs.
        /// </remarks>
        public IPlantSimulatorOptions Options { get; set; }
    }
}