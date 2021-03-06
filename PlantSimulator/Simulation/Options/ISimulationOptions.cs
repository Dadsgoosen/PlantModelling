﻿namespace PlantSimulator.Simulation.Options
{
    public interface ISimulationOptions
    {
        public uint TickTime { get; }
        public uint TickEventTime { get; }
        public int RandomSeed { get; }
        public uint StopAtTick { get; }
    }
}