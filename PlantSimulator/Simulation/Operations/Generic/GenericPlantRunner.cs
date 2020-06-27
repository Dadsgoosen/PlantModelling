using System;
using System.Collections.Generic;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.Plants.Fluids;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericPlantRunner : IPlantRunner
    {
        private readonly IPlantGrower plantGrower;

        private readonly FluidsPlantCycle sucroseCycle;

        public IPlant Plant { get; }

        public SimulationEnvironment Environment { get; }

        public GenericPlantRunner(IPlant plant, SimulationEnvironment environment, IPlantGrower plantGrower, FluidsPlantCycle sucroseCycle)
        {
            this.plantGrower = plantGrower;
            this.sucroseCycle = sucroseCycle;
            Plant = plant;
            Environment = environment;
        }

        public void Tick(SimulationStateSnapshot stateSnapshot)
        {
            if (ShouldSpawnSucrose(stateSnapshot))
            {
                sucroseCycle.SpawnSucrose();
            }

            plantGrower.GrowPlant(Plant, stateSnapshot);
        }

        private bool ShouldSpawnSucrose(SimulationStateSnapshot snapshot)
        {
            if (snapshot.CurrentTime == 0) return true;

            int count = 1;

            Stack<IPlantPart> parts = new Stack<IPlantPart>(new[] { Plant.ShootSystem.Stem.Internode });

            while (parts.Count > 0)
            {
                var part = parts.Pop();

                foreach (var conn in part.Connections)
                {
                    parts.Push(conn);
                }

                if (part.PartType == PlantPartType.Petiole)
                {
                    count++;
                }
            }

            int frequency = 352 - (24 * count);

            return (int) snapshot.CurrentTime % Math.Max(frequency, 1) == 0;
        }
    }
}