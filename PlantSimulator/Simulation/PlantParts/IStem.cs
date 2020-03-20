namespace PlantSimulator.Simulation.PlantParts
{
    public interface IStem : IPlantPart
    {
        public IInternode Internode { get; }
    }
}