namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Leaf : PlantPart
    {
        public override PlantPartType PartType { get; }

        /// <summary>
        /// The petiole that this leaf is connected to
        /// </summary>
        public Petiole Petiole { get; protected set; }

        protected Leaf()
        {
            PartType = PlantPartType.Leaf;
        }
    }
}