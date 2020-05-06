namespace PlantSimulator.Simulation.Options
{
    public class SimulationOptions
    {
        public uint TickTime { get; set; }

        private uint tickEventTime = 1;

        public uint TickEventTime
        {
            get => tickEventTime;
            set => tickEventTime = value <= 0 ? 1 : value;
        }
    }
}