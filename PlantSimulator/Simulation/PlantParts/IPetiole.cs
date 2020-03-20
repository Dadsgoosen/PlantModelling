namespace PlantSimulator.Simulation.PlantParts
{
    /// <summary>
    /// Is a stem support 
    /// </summary>
    public interface IPetiole : IPlantPart
    {
        /// <summary>
        /// The angle between the shoot stem and the petiole, branch or bud
        /// </summary>
        public float Axil { get; }

        /// <summary>
        /// The node that the petiole is connected to
        /// </summary>
        public INode Node { get; }
    }
}