using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericPlantGrower : IPlantGrower
    {
        private readonly ICellGrower cellGrower;

        private readonly IPlantSimulatorOptions options;

        private SimulationStateSnapshot currentState;

        private readonly SimulationEnvironment environment;

        public GenericPlantGrower(ICellGrower cellGrower, IPlantSimulatorOptions options, SimulationEnvironment environment)
        {
            this.environment = environment;
            this.cellGrower = cellGrower;
            this.options = options;
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
            var po = options.Plant;

            float topX = float.MinValue;
            float topY = float.MinValue;
            float topZ = float.MinValue;
            float lowestX = float.MaxValue;
            float lowestY = float.MaxValue;
            float lowestZ = float.MaxValue;

            foreach (var cell in part.Cells)
            {
                cellGrower.GrowShootCell(cell, part, currentState);

                var geo = cell.Geometry;

                topX = Math.Max(geo.TopCenter.X, topX);
                topY = Math.Max(geo.TopCenter.Y, topY);
                topZ = Math.Max(geo.TopCenter.Z, topZ);
                lowestX = Math.Min(geo.BottomCenter.X, lowestX);
                lowestY = Math.Min(geo.BottomCenter.Y, lowestY);
                lowestZ = Math.Min(geo.BottomCenter.Z, lowestZ);
            }

            if (part.PartType == PlantPartType.Internode && part is Internode s && !s.HasUpperNode())
            {
                var top = new Vector3(topX, topY, topZ);
                var bottom = new Vector3(lowestX, lowestY, lowestZ);

                var height = Vector3.Distance(top, bottom);

                
            }


        }

        private void HandleRootPart(IPlantPart part)
        {r
        }

        private void HandleRootCells(IPlantPart plantPart)
        {
            foreach (var cell in plantPart.Cells)
            {
                cellGrower.GrowRootCell(cell, plantPart, currentState);
            }
        }

        private void HandleShootCells(IPlantPart plantPart)
        {
            foreach (var cell in plantPart.Cells)
            {
                cellGrower.GrowShootCell(cell, plantPart, currentState);
            }
        }
    }
}