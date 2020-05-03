namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericPlant : IPlant
    {
        public IShootSystem ShootSystem { get; }
     
        public IRootSystem RootSystem { get; }

        public GenericPlant() { }

        public GenericPlant(IShootSystem shootSystem, IRootSystem rootSystem)
        {
            ShootSystem = shootSystem;
            RootSystem = rootSystem;
        }
    }
}