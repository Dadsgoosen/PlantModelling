namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericRootSystem : IRootSystem
    {
        public Root PrimaryRoot { get; }

        public GenericRootSystem(Root primaryRoot)
        {
            PrimaryRoot = primaryRoot;
        }
    }
}