namespace PlantSimulator.Outputs.Models
{
    /// <summary>
    /// Data Access Object for a simulation state in time
    /// </summary>
    public class SimulationState
    {
        /// <summary>
        /// Unique ID for a specific simulation
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Tick timer for the simulation state
        /// </summary>
        public long SimulationTime { get; set; }

        /// <summary>
        /// Plant Model Description for the Shoot and Root System
        /// </summary>
        public PlantModelState Plant { get; set; }
    }
}