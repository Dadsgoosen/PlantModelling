namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericRootSystem : IRootSystem
    {
        public IRoot PrimaryRoot { get; }

        public GenericRootSystem(IRoot primaryRoot)
        {
            PrimaryRoot = primaryRoot;
        }
    }
}