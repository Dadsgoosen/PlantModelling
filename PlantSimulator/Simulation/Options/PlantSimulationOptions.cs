namespace PlantSimulator.Simulation.Options
{
    public class PlantSimulationOptions : IPlantSimulatorOptions
    {
        /// <summary>
        /// Unique simulation id for which this <see cref="PlantSimulationOptions"/> is for
        /// </summary>
        public string Id { get; set; }
        
        /// <summary>
        /// The options that involve how the simulation runs
        /// </summary>
        public ISimulationOptions Simulation { get; set; }

        /// <summary>
        /// The options that involve plant behaviour during simulation
        /// </summary>
        public ISimulationPlantOptions Plant { get; set; }

        /// <summary>
        /// The options that involve the plant cells
        /// </summary>
        public ISimulationCellOptions Cell { get; set; }

        /// <summary>
        /// The options that involve the external environment
        /// </summary>
        public ISimulationEnvironmentOptions Environment { get; set; }
    }
}