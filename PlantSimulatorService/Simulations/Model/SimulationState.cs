using System;

namespace PlantSimulatorService.Simulations.Model
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
        public ulong SimulationTime { get; set; }

        /// <summary>
        /// DateTime for when the simulation was last saved
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Plant Model Description for the Shoot and Root System
        /// </summary>
        public PlantModel Plant { get; set; }
    }
}