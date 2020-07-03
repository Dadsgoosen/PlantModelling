using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Cells.Storage;
using PlantSimulator.Simulation.Operations.Development;
using PlantSimulator.Simulation.Operations.Transporters;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericPlantGrower : IPlantGrower
    {
        private readonly ILoggerAdapter<GenericPlantRunner> logger;

        private readonly ICellBodySystemSolver cellBodySystem;

        private readonly IPlantPartDeveloper plantPartDeveloper;

        private readonly FluidTransporter<Sucrose> sucroseTransporter;

        private SimulationStateSnapshot currentState;

        public GenericPlantGrower(ICellBodySystemSolver cellBodySystem, 
            IPlantPartDeveloper plantPartDeveloper, 
            FluidTransporter<Sucrose> sucroseTransporter, 
            ILoggerAdapter<GenericPlantRunner> logger)
        {
            this.cellBodySystem = cellBodySystem;
            this.plantPartDeveloper = plantPartDeveloper;
            this.sucroseTransporter = sucroseTransporter;
            this.logger = logger;
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

                PushConnections(part.Connections, postponedParts);

                sucroseTransporter.Transport(part);

                HandlePlantPart(part, isShoot);

                cellBodySystem.Solve(part);
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

        private void PushConnections(IEnumerable<IPlantPart> connections, Stack<IPlantPart> partStack)
        {
            foreach (var connection in connections)
            {
                partStack.Push(connection);
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