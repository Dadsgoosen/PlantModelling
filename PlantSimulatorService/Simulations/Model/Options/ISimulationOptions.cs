namespace PlantSimulatorService.Simulations.Model.Options
{
    public interface ISimulationOptions
    {
        public uint TickTime { get; }
        public uint TickEventTime { get; }
        public int RandomSeed { get; }
    }
}