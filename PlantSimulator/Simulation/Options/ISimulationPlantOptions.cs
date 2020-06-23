using PlantSimulator.Helpers;

namespace PlantSimulator.Simulation.Options
{
    public interface ISimulationPlantOptions
    {
        /// <summary>
        /// The initial stem width
        /// </summary>
        public int InitialStemWidth { get; set; }

        /// <summary>
        /// The rate of which the plant grows.
        /// </summary>
        public Range<float> GrowthRange { get; set; }

        /// <summary>
        /// The maximum height of a plant
        /// </summary>
        public Range<float> TerminalHeight { get; set; }

        /// <summary>
        /// The maximum depth/length of roots
        /// </summary>
        public Range<float> TerminalRootLength { get; set; }

        /// <summary>
        /// The tick count when to stop root growth
        /// </summary>
        public Range<int> RootTickStopGrowth { get; set; }

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
        public Range<float> MaxInternodeLength { get; set; }

        /// <summary>
        /// How long does an internode need to be before there
        /// is a chance for a new node to be created
        /// </summary>
        public Range<float> NewNodeInternodeLength { get; set; }

        /// <summary>
        /// How many ticks before a new node i created
        /// </summary>
        public Range<int> NewNodeTickCount { get; set; }

        /// <summary>
        /// How many leafs per node
        /// </summary>
        public Range<int> LeafsPerNode { get; set; }

        /// <summary>
        /// A range defining the length of each petiole
        /// </summary>
        public Range<int> PetioleLength { get; set; }

        /// <summary>
        /// The width of newly spawned petioles
        /// </summary>
        public Range<int> NewPetioleWidth { get; set; }

        /// <summary>
        /// How many petioles gets created per new node
        /// </summary>
        public Range<int> PetiolesPerNode { get; set; }

        /// <summary>
        /// How many stems gets created per new node
        /// </summary>
        public Range<int> StemsPerNode { get; set; }

        /// <summary>
        /// The width of newly spawned stems
        /// </summary>
        public Range<int> NewStemWidth { get; set; }

        /// <summary>
        /// The length of a root, be it primary or lateral, before it creates new roots
        /// </summary>
        public Range<float> NewRootLength { get; set; }

        /// <summary>
        /// The tick count of the simulation, before it creates new roots
        /// </summary>
        public Range<int> NewRootTick { get; set; }

        /// <summary>
        /// The angle between a stem and the petiole
        /// </summary>
        public int Axil { get; set; }
    }
}