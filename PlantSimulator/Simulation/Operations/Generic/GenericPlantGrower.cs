using System;
using System.Collections;
using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericPlantGrower : IPlantGrower
    {
        private readonly ICellGrower cellGrower;

        private SimulationStateSnapshot currentState;

        private readonly SimulationEnvironment environment;

        public GenericPlantGrower(ICellGrower cellGrower, SimulationEnvironment environment)
        {
            this.environment = environment;
            this.cellGrower = cellGrower;
        }

        public void GrowPlant(IPlant plant, SimulationStateSnapshot stateSnapshot)
        {
            currentState = stateSnapshot;
            IterateShootSystem(plant.ShootSystem);
            IterateRootSystem(plant.RootSystem);
        }

        private void IterateRootSystem(IRootSystem rootSystem)
        {
            IteratePlantParts((IPlantPart)rootSystem.PrimaryRoot, false);
        }

        private void IterateShootSystem(IShootSystem shootSystem)
        {
            IteratePlantParts((IPlantPart)shootSystem.Stem, true);
        }

        private void IteratePlantParts(IPlantPart start, bool isShoot)
        {
            Stack<IPlantPart> postponedParts = new Stack<IPlantPart>(new[] { start });

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
                foreach (var cell in plantPart.Cells)
                {
                    HandleShootCell(cell, plantPart);
                }
            }
            else
            {
                foreach (var cell in plantPart.Cells)
                {
                    HandleRootCell(cell, plantPart);
                }
            }
        }

        private void HandleRootCell(IPlantCell rootCell, IPlantPart plantPart)
        {
            cellGrower.GrowRootCell(rootCell, plantPart, currentState);
        }

        private void HandleShootCell(IPlantCell shootCell, IPlantPart plantPart)
        {
            cellGrower.GrowShootCell(shootCell, plantPart, currentState);
        }
    }
}