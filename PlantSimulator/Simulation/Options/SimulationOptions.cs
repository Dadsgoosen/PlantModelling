using System;

namespace PlantSimulator.Simulation.Options
{
    public class SimulationOptions : ISimulationOptions
    {
        public uint TickTime { get; set; }

        private uint tickEventTime = 1;

        public uint TickEventTime
        {
            get => tickEventTime;
            set => tickEventTime = value <= 0 ? 1 : value;
        }

        public int RandomSeed { get; set; } = new Random().Next();

        public uint StopAtTick { get; set; } = uint.MaxValue;
    }
}