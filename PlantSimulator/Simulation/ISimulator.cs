using System;

namespace PlantSimulator.Simulation
{
    public interface ISimulator : IDisposable
    {
        void Start();
        void Stop();
    }
}