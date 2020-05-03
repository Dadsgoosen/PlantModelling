namespace PlantSimulator.Simulation.PlantParts
{
    public interface IInternode 
    {
        public INode UpperNode { get; }

        public INode LowerNode { get; }

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