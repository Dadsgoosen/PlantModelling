using System;

namespace PlantSimulator.Simulation.Cells
{
    public interface IVacuole
    {
        public float Volume { get; set; }

        public float TurgorPressure { get; set; }
    }
}