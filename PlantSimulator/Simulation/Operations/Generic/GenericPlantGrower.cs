using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Operations.Development;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericPlantGrower : IPlantGrower
    {
        private readonly ICellGrower cellGrower;

        private readonly IPlantPartDeveloper plantPartDeveloper;

        private SimulationStateSnapshot currentState;

        public GenericPlantGrower(ICellGrower cellGrower, IPlantPartDeveloper plantPartDeveloper)
        {
            this.cellGrower = cellGrower;
            this.plantPartDeveloper = plantPartDeveloper;
        }

        public void GrowPlant(IPlant plant, SimulationStateSnapshot stateSnapshot)
        {
            currentState = stateSnapshot;
            IterateShootSystem(plant.ShootSystem);
            IterateRootSystem(plant.RootSystem);
        }

        private void IterateRootSystem(IRootSystem rootSystem)
        {
            IteratePlantParts(rootSystem.PrimaryRoot, false);
        }

        private void IterateShootSystem(IShootSystem shootSystem)
        {
            IteratePlantParts(shootSystem.Stem, true);
        }

        private void IteratePlantParts(IPlantPart start, bool isShoot)
        {
            var postponedParts = new Stack<IPlantPart>(new[] { start });

            while (postponedParts.Count > 0)
            {
                IPlantPart part = postponedParts.Pop();

                HandlePlantPart(part, isShoot);

                foreach (var connection in part.Connections)
                {
                    postponedParts.Push(connection);
                }
            }
        }

        private void HandlePlantPart(IPlantPart plantPart, bool isShoot)
        {
            if (isShoot)
            {
                HandleShootPart(plantPart);
            }
            else
            {
                HandleRootPart(plantPart);
            }
        }

        private void HandleShootPart(IPlantPart part)
        {
            plantPartDeveloper.Develop(part, currentState);
        }

        private void HandleRootPart(IPlantPart part)
        {
            plantPartDeveloper.Develop(part, currentState);
        }
    }
}