using System;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation
{
    public class PlantSimulatorTickEvent : EventArgs
    {
        public IPlant Plant { get; }

        public PlantSimulatorTickEvent(IPlant plant)
        {
            Plant = plant;
        }
    }
}