using System;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Cells
{
    public class Vacuole : IVacuole
    {
        private const float Capacity = 100;

        public IStorage<Water> Volume { get; }

        public float TurgorPressure => Math.Min(1, Volume.Amount / Capacity);

        public Vacuole()
        {
            Volume = new WaterStorage(0, Capacity);
        }
    }
}