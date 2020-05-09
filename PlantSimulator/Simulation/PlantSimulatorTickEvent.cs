using System;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation
{
    public class PlantSimulatorTickEvent : EventArgs
    {
        public string Id { get; }
        
        public IPlant Plant { get; }

        public ulong TickTimer { get; }

        public PlantSimulatorTickEvent(string id, IPlant plant, ulong tickTimer)
        {
            Id = id;
            Plant = plant;
            TickTimer = tickTimer;
        }
    }
}