namespace PlantSimulator.Simulation.PlantParts
{
    public abstract class Internode : PlantPart
    {
        public Node UpperNode { get; }

        public Node LowerNode { get; }

        public bool HasUpperNode()
        {
            return UpperNode != null;
        }

        public bool HasLowerNode()
        {
            return LowerNode != null;
        }
    }
}