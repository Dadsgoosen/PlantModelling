using PlantSimulator.Helpers;

namespace PlantSimulator.Simulation.Options
{
    public class SimulationPlantOptions
    {
        /// <summary>
        /// How many branches can a stem create
        /// </summary>
        public Range<int> Branches { get; set; }

        /// <summary>
        /// How many times can the main stem branch out
        /// </summary>
        public Range<int> SubBranches { get; set; }

        /// <summary>
        /// A range defining the length that each internode can be
        /// </summary>
        public Range<int> InternodeLength { get; set; }

        /// <summary>
        /// The angle between a stem and the petiole
        /// </summary>
        public int Axil { get; set; }
    }
}