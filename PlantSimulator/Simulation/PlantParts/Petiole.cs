namespace PlantSimulator.Simulation.PlantParts
{
    /// <summary>
    /// Is a stem support 
    /// </summary>
    public abstract class Petiole : PlantPart
    {
        public override PlantPartType PartType { get; }

        /// <summary>
        /// The angle between the shoot stem and the petiole, branch or bud
        /// </summary>
        public float Axil { get; }

        /// <summary>
        /// The node that the petiole is connected to
        /// </summary>
        public Node Node { get; }

        protected Petiole()
        {
            PartType = PlantPartType.Petiole;
        }
    }
}