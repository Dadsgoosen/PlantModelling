using System;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation
{
    public class PlantSimulatorTickEvent : EventArgs
    {
        public IPlant Plant { get; }

        public ulong TickTimer { get; }

        public PlantSimulatorTickEvent(IPlant plant, ulong tickTimer)
        {
            Plant = plant;
            TickTimer = tickTimer;
        }
    }
}