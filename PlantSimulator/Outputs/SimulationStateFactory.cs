 using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using PlantSimulator.Outputs.Models;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Outputs
{
    public class SimulationStateFactory : ISimulationStateFactory
    {
        public SimulationState Create(string id, IPlant plant, SimulationStateSnapshot data)
        {
            return new SimulationState
            {
                Id = id,
                SimulationTime = data.CurrentTime,
                Plant = CreatePlantState(plant)
            };
        }

        private static PlantModelState CreatePlantState(IPlant plant)
        {
            return new PlantModelState
            {
                ShootSystem = CreateShootState(plant.ShootSystem),
                RootSystem = CreateRootState(plant.RootSystem)
            };
        }

        private static IEnumerable<PlantNodeModelState> CreateShootState(IShootSystem shootSystem)
        {
            return IterateSystem((IPlantPart) shootSystem.Stem);
        }

        private static IEnumerable<PlantNodeModelState> CreateRootState(IRootSystem rootSystem)
        {
            return IterateSystem((IPlantPart) rootSystem.PrimaryRoot);
        }

        private static IEnumerable<PlantNodeModelState> IterateSystem(IPlantPart startPart)
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

        private static PlantNodeModelState CreateStateFromPlantPart(IPlantPart part)
        {
            var highest = Vector2.Zero;
            var lowest = Vector2.Zero;
            var widest = new[] {Vector2.Zero, Vector2.Zero};

            foreach (var cell in part.Cells)
            {
                var top = cell.Geometry.TopCenter;
                var bottom = cell.Geometry.BottomCenter;

                if (top.Y > highest.Y)
                {
                    highest = new Vector2(top.X, top.Y);
                }

                if (bottom.Y < lowest.Y)
                {
                    lowest = new Vector2(bottom.X, bottom.Y);
                }

                if (top.X > widest[1].X)
                {
                    widest[1] = new Vector2(top.X, top.Y);
                }

                if (bottom.X < widest[0].X)
                {
                    widest[0] = new Vector2(bottom.X, bottom.Y);
                }
            }

            IList<PlantNodeModelState> connections = new List<PlantNodeModelState>(part.Connections.Count());

            foreach (var connection in part.Connections)
            {
                connections.Add(CreateStateFromPlantPart(connection));
            }

            return new PlantNodeModelState
            {
                Thickness = (int) Vector2.Distance(widest[0], widest[1]),
                Connections = connections,
                Coordinates = new[] {lowest, highest}
            };
        }
    }
}