namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericPlant : IPlant
    {
        public IShootSystem ShootSystem { get; }
        public IRootSystem RootSystem { get; }
    }
}