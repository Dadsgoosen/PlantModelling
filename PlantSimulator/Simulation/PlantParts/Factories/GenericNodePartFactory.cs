using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.InteropServices.ComTypes;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public class GenericNodePartFactory : INodePartFactory
    {
        private readonly Random random;

        private readonly IPlantPartCellCreator cellCreator;

        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly IStemPartFactory stemFactory;

        private readonly IPetiolePartFactory petioleFactory;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public GenericNodePartFactory(IPlantSimulatorOptionsService optionsService, IPlantPartCellCreator cellCreator, 
            IStemPartFactory stemFactory, IPetiolePartFactory petioleFactory)
        {
            this.cellCreator = cellCreator;
            this.optionsService = optionsService;
            this.stemFactory = stemFactory;
            this.petioleFactory = petioleFactory;
            random = new Random(Options.Simulation.RandomSeed);
        }

        public GenericNode CreateNode(Internode lowerInternode, IPlantPartDescriptor descriptor, bool withOptions)
        {
            var cells = CreateNodeCells(descriptor);

            var node = new GenericNode(lowerInternode, cells, lowerInternode.BranchCount);

            // If the node is created according to the options, then
            // the created stems and petioles will be added to the lists
            // above, otherwise they will just be empty lists.
            if (withOptions)
            {
                node.Stems = CreateStems(descriptor, node);
                node.Petioles = CreatePetioles(descriptor);
            }

            return node;
        }

        private IEnumerable<IPlantCell> CreateNodeCells(IPlantPartDescriptor descriptor)
        {
            var width = descriptor.WidthX;
            var depth = descriptor.WidthZ;
            var center = descriptor.Top;

            return cellCreator.CreateCells(width, depth, center, 0);
        }

        private IEnumerable<Petiole> CreatePetioles(IPlantPartDescriptor descriptor)
        {
            var amount = Options.Plant.PetiolesPerNode.RandomNumberBetween();

            Petiole[] petioles = new Petiole[amount];

            for (int i = 0; i < amount; i++)
            {
                petioles[i] = CreatePetiole(descriptor);
            }

            return petioles;
        }

        private IEnumerable<Stem> CreateStems(IPlantPartDescriptor descriptor, Node node)
        {
            var amount = Options.Plant.StemsPerNode.RandomNumberBetween();

            Stem[] stems = new Stem[amount];

            for (int i = 0; i < amount; i++)
            {
                stems[i] = CreateStem(descriptor, node);
            }

            return stems;
        }

        private Stem CreateStem(IPlantPartDescriptor descriptor, Node node)
        {
            var center = CreateStemCenterVector(descriptor);

            return stemFactory.CreateStem(center, node);
        }

        private Petiole CreatePetiole(IPlantPartDescriptor descriptor)
        {
            bool spawnOnLeft = ShouldSpawnOnLeftSide();

            Vector3 center;

            if (spawnOnLeft)
            {
                center = new Vector3(descriptor.MinX, descriptor.Top.Y, descriptor.Top.Z);
            }
            else
            {
                center = new Vector3(descriptor.MaxX, descriptor.Top.Y, descriptor.Top.Z);
            }

            var length = Options.Plant.PetioleLength.RandomNumberBetween();

            Vector3 direction = center + new Vector3(length, length, 0);

            return petioleFactory.CreatePetiole(center, direction);
        }

        private Vector3 CreateStemCenterVector(IPlantPartDescriptor descriptor)
        {
            bool spawnOnLeft = ShouldSpawnOnLeftSide();

            Vector3 center;

            if (spawnOnLeft)
            {
                center = new Vector3(descriptor.MinX, descriptor.Top.Y, descriptor.Top.Z);
            }
            else
            {
                center = new Vector3(descriptor.MaxX, descriptor.Top.Y, descriptor.Top.Z);
            }

            return center;
        }

        private bool ShouldSpawnOnLeftSide()
        {
            return random.NextDouble() > .5;
        }
    }
}