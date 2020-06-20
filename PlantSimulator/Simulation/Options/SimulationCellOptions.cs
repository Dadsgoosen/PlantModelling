namespace PlantSimulator.Simulation.Options
{
    public class SimulationCellOptions : ISimulationCellOptions
    {
        /// <summary>
        /// Size of the parenchyma cell
        /// </summary>
        public float ParenchymaCellSize { get; set; }

        /// <summary>
        /// Size of the collenchyma cell
        /// </summary>
        public float CollenchymaCellSize { get; set; }

        /// <summary>
        /// Size of the xylem cell
        /// </summary>
        public float XylemCellSize { get; set; }

        /// <summary>
        /// Size of the phloem cell
        /// </summary>
        public float PhloemCellSize { get; set; }

        /// <summary>
        /// Size of the sclerenchyma cell
        /// </summary>
        public float SclerenchymaCellSize { get; set; }
    }
}