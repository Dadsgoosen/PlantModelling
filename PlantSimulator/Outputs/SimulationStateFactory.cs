 using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using PlantSimulator.Outputs.Models;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.PlantParts;
 using PlantSimulator.Simulation.PlantParts.Helpers;

 namespace PlantSimulator.Outputs
{
    public class SimulationStateFactory : ISimulationStateFactory
    {
        private readonly HashSet<PlantPartType> SkipTypes;

        private readonly IPlantDescriptorService descriptorService;

        public SimulationStateFactory(IPlantDescriptorService descriptorService)
        {
            this.descriptorService = descriptorService;
            SkipTypes = new HashSet<PlantPartType>(new []{PlantPartType.Stem, PlantPartType.Node});
        }

        public SimulationState Create(string id, IPlant plant, SimulationStateSnapshot data)
        {
            return new SimulationState
            {
                Id = id,
                SimulationTime = data.CurrentTime,
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
            return IterateSystem(shootSystem.Stem);
        }

        private IEnumerable<PlantNodeModelState> CreateRootState(IRootSystem rootSystem)
        {
            return IterateSystem(rootSystem.PrimaryRoot);
        }

        private IEnumerable<PlantNodeModelState> IterateSystem(IPlantPart startPart)
        {
            IList<PlantNodeModelState> states = new List<PlantNodeModelState>();

            Stack<IPlantPart> stack = new Stack<IPlantPart>(new[] { startPart });

            while (stack.Count > 0)
            {
                var part = stack.Pop();

                if (!SkipTypes.Contains(part.PartType))
                {
                    states.Add(CreateStateFromPlantPart(part));
                }

                foreach (var connection in part.Connections)
                {
                    stack.Push(connection);
                }
            }

            return states;
        }

        private PlantNodeModelState CreateStateFromPlantPart(IPlantPart part)
        {
            var asTop = part.PartType == PlantPartType.Root;
            var descriptor = descriptorService.Describe(part, asTop);

            var top3 = descriptor.Top;
            var bot3 = descriptor.Bottom;

            var top = new Vector2(top3.X, top3.Y);
            var bot = new Vector2(bot3.X, bot3.Y);

            return new PlantNodeModelState
            {
                Thickness = ComputeThickness(descriptor),
                Description = $"{part.PartType} ({part.BranchCount})",
                Connections = new PlantNodeModelState[0],
                Coordinates = new[] {bot, top}
            };
        }

        private float ComputeThickness(IPlantPartDescriptor descriptor)
        {
            var top = descriptor.Top;
            return Vector2.Distance(new Vector2(top.X, top.Y), new Vector2(descriptor.MaxX, top.Y));
        }

        private int GetThickness(IPlantPart part)
        {
            switch (part.PartType)
            {
                case PlantPartType.Stem when part.BranchCount > 0:
                    return 7;
                case PlantPartType.Petiole:
                    return 5;
                case PlantPartType.Root:
                    return 7;
                default:
                    return 10;
            }
        }
    }
}