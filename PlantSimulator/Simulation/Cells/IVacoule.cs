using System;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Cells
{
    public interface IVacuole
    {
        public IStorage<Water> Volume { get; }

        public float TurgorPressure { get; }
    }
}