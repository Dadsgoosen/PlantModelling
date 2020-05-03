
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using PlantSimulator.Outputs.Models;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Outputs
{
    public class SimulationStateFactory : ISimulationStateFactory
    {
        public SimulationState Create(IPlant plant, SimulationStateData data)
        {
            return new SimulationState
            {
                Id = data.Id,
                SimulationTime = data.SimulationTimer,
                Plant = CreatePlantState(plant)
            };
        }

        private PlantModelState CreatePlantState(IPlant plant)
        {
            return new PlantModelState
            {
                ShootSystem = CreateShootState(plant.ShootSystem),
                RootSystem = CreateRootState(plant.RootSystem)
            };
        }

        private IEnumerable<PlantNodeModelState> CreateShootState(IShootSystem shootSystem)
        {
            return IterateSystem((IPlantPart) shootSystem.Stem);
        }

        private IEnumerable<PlantNodeModelState> CreateRootState(IRootSystem rootSystem)
        {
            return IterateSystem((IPlantPart) rootSystem.PrimaryRoot);
        }

        private IEnumerable<PlantNodeModelState> IterateSystem(IPlantPart startPart)
        {
            IList<PlantNodeModelState> states = new List<PlantNodeModelState>();

            Stack<IPlantPart> stack = new Stack<IPlantPart>(new[] { startPart });

            while (stack.Count > 0)
            {
                var part = stack.Pop();

                states.Add(CreateStateFromPlantPart(part));

                foreach (var connection in part.Connections)
                {
                    stack.Push(connection);
                }
            }

            return states;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private PlantNodeModelState CreateStateFromPlantPart(IPlantPart part)
        {
            Vector3 partCenter = Vector3.Zero;

            int cellCount = 0;

            foreach (var cell in part.Cells)
            {
                var center = cell.Geometry.TopCenter;
                partCenter += new Vector3(center.X, center.Y, center.Z);
                cellCount++;
            }

            partCenter /= cellCount;

            var furthestZ = 0F;
            var furthestX = 0F;

            Vector3 top = new Vector3(partCenter.X, 0, partCenter.Z);
            Vector3 bottom = new Vector3(partCenter.X, 0, partCenter.Z);

            foreach (var cell in part.Cells)
            {
                var geo = cell.Geometry.TopCenter;
                
                var distZ = (float) Math.Sqrt(Math.Pow(geo.Z - partCenter.Z, 2));
                var distX = (float) Math.Sqrt(Math.Pow(geo.X - partCenter.X, 2));

                if (distZ > furthestZ)
                {
                    furthestZ = distZ;
                }

                if (distX > furthestX)
                {
                    furthestX = distX;
                }

                if (geo.Y > top.Y)
                {
                    top.X = geo.Y;
                }

                if (geo.Y < bottom.Y)
                {
                    bottom.Y = geo.Y;
                }
            }

            var connections = new List<PlantNodeModelState>();

            foreach (var connection in part.Connections)
            {
                connections.Add(CreateStateFromPlantPart(connection));
            }

            return new PlantNodeModelState
            {
                Coordinates = new [] {bottom, top},
                Thickness = (int) ((furthestZ + furthestX) / 2),
                Connections = new PlantNodeModelState[0]
            };
        }
    }
}