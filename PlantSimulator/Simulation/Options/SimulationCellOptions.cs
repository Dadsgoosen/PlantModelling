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

        /// <summary>
        /// The minimum vacuole capacity before a cell begins
        /// to request more water
        /// </summary>
        public float MinimumVacuoleBeforeRequest { get; set; }

        /// <summary>
        /// The speed of which water moves
        /// </summary>
        public uint WaterSpeed { get; set; }

        /// <summary>
        /// The speed of which sucrose moves
        /// </summary>
        public uint SucroseSpeed { get; set; }
    }
}