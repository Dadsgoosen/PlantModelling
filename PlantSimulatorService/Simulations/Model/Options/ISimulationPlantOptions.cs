namespace PlantSimulatorService.Simulations.Model.Options
{
    public interface ISimulationPlantOptions
    {
        /// <summary>
        /// How many branches can a stem create
        /// </summary>
        public Range<int> Branches { get; }

        /// <summary>
        /// How many times can the main stem branch out
        /// </summary>
        public Range<int> SubBranches { get; }

        /// <summary>
        /// A range defining the length that each internode can be
        /// </summary>
        public Range<int> InternodeLength { get; }

        /// <summary>
        /// A range defining the length of each petiole
        /// </summary>
        public Range<int> PetioleLength { get; set; }

        /// <summary>
        /// The angle between a stem and the petiole
        /// </summary>
        public int Axil { get; }
    }
}