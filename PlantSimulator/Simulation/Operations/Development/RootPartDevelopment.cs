using System;
using System.Linq;
using System.Numerics;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells.Helpers;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Operations.Development
{
    public class RootPartDevelopment : IPlantPartDevelopment<Root>
    {
        private readonly Random random;

        private readonly IPlantSimulatorOptionsService optionsService;

        private IPlantSimulatorOptions Options => optionsService.Options;

        private readonly INodePartFactory nodePartFactory;

        private readonly IRootPartFactory rootFactory;

        private readonly ICellGrower cellGrower;

        private readonly IPlantDescriptorService plantDescriptorService;

        public RootPartDevelopment(IPlantSimulatorOptionsService optionsService, 
            INodePartFactory nodePartFactory, 
            IRootPartFactory rootFactory,
            ICellGrower cellGrower, 
            IPlantDescriptorService plantDescriptorService)
        {
            this.optionsService = optionsService;
            this.nodePartFactory = nodePartFactory;
            this.rootFactory = rootFactory;
            this.cellGrower = cellGrower;
            this.plantDescriptorService = plantDescriptorService;
            random = new Random(optionsService.Options.Simulation.RandomSeed);
        }

        public void Develop(Root plantPart, SimulationStateSnapshot snapshot)
        {
            var descriptor = plantDescriptorService.Describe(plantPart, false);

            if (ShouldGrow(plantPart, descriptor, snapshot))
            {
                CellIterator.IterateCells(plantPart.Cells, cell => cellGrower.GrowRootCell(cell, plantPart, snapshot));
            }

            if (ShouldAddLateralRoot(plantPart, descriptor, snapshot))
            {
                var newRoot = CreateNewRoot(descriptor, plantPart);

                plantPart.ConnectRoot(newRoot);
            }
        }

        private bool ShouldGrow(Root root, IPlantPartDescriptor descriptor, SimulationStateSnapshot snapshot)
        {
            var atHeight = descriptor.Height >= Options.Plant.TerminalHeight.RandomNumberBetween();

            var atTick = snapshot.CurrentTime >= (uint) Options.Plant.RootTickStopGrowth.RandomNumberBetween();

            var atTerminalHeight = descriptor.Height >= Options.Plant.TerminalRootLength.RandomNumberBetween();

            return !atTerminalHeight || !(atHeight || atTick);
        }

        private bool ShouldAddLateralRoot(Root root, IPlantPartDescriptor descriptor, SimulationStateSnapshot snapshot)
        {
            // Determine if it should add new lateral root based on tick count
            ulong tick = snapshot.CurrentTime;

            uint tickCount = (uint) Options.Plant.RootTickStopGrowth.RandomNumberBetween();

            bool atTick;
            
            if (tickCount > 0)
            {
                atTick = tick % tickCount == 0 && tick > 0;
            }
            else
            {
                atTick = tick > 0;
            }

            // Determine if it should add new lateral root based on length
            float newRootHeight = Options.Plant.NewRootLength.RandomNumberBetween();

            int rootCount = root.Connections.Count() + 1;

            float atHeight = descriptor.Height / newRootHeight;

            bool isAtHeight = atHeight > rootCount;

            return atTick || isAtHeight;
        }

        private Root CreateNewRoot(IPlantPartDescriptor descriptor, Root root)
        {
            var center = CreateNewRootCenter(descriptor);

            var branchCount = root.BranchCount + 1;

            var newRoot = rootFactory.CreateRoot(center, branchCount);

            return newRoot;
        }

        private Vector3 CreateNewRootCenter(IPlantPartDescriptor descriptor)
        {
            bool left = ShouldSpawnOnLeft();

            float x = left ? descriptor.MinX : descriptor.MaxX;
            float y = descriptor.Bottom.Y;
            float z = descriptor.Bottom.Z;

            return new Vector3(x, y, z);
        }

        private bool ShouldSpawnOnLeft()
        {
            return random.NextDouble() > 0.5;
        }
    }
}