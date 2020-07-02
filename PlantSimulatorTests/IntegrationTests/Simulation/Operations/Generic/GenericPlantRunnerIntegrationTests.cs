using System;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PlantSimulator;
using PlantSimulator.Helpers;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Cells.Storage;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.Operations.Carriers;
using PlantSimulator.Simulation.Operations.Development;
using PlantSimulator.Simulation.Operations.Transporters;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Helpers;
using PlantSimulator.Simulation.Plants.Corn;
using PlantSimulator.Simulation.Plants.Fluids;

namespace PlantSimulatorTests.IntegrationTests.Simulation.Operations.Generic
{
    [TestFixture]
    public class GenericPlantRunnerIntegrationTests
    {
        private SimulationEnvironment environment;

        private IPlant plant;

        private IPlantSimulatorOptionsService optionsService;

        private ICellFactory cellFactory;

        private ICellCollisionDetection cellCollisionDetection;

        private ICellBodySystemSolver bodySystemSolver;

        private ICellDivider divider;

        private ICellGrower cellGrower;

        private IPlantGrower plantGrower;

        private IPlantRunner runner;

        private IGeometryHelper helper;

        private ICellSizer cellSizer;

        private IPlantPartDeveloper developer;

        private IPlantPartDevelopment<Internode> internodePlantPartDevelopment;

        private IPlantDescriptorService descriptorService;

        private ISingularCellCreator cellCreator;

        private ICellGridFactory gridCreator;

        private IPlantPartCellCreator plantPartCellCreator;

        private INodePartFactory nodePartFactory;
        
        private IInternodePartFactory internodePartFactory;

        private IStemPartFactory stemPartFactory;

        private IPetiolePartFactory petiolePartFactory;

        private IPlantPartDevelopment<Root> rootPlantPartDevelopment;

        private IRootPartFactory rootFactory;

        private FluidTransporter<Sucrose> sucroseTransporter;

        private CarrierCollection<Sucrose> sucroseCarrierCollection;

        [SetUp]
        public void Setup()
        {
            environment = new SimulationEnvironment {LightPosition = new Vertex(0, 10000, 0), Temperature = 22};
            plant = TestPlant.CreatePlant();
            cellFactory = new GenericCellFactory();
            optionsService = PlantSimulatorOptionsHelper.CreateOptionsService();
            divider = new GenericCellDivider(cellFactory);
            helper = new GeometryHelper();
            cellSizer = new GenericCellSizer(helper, new LoggerAdapter<GenericCellSizer>(new NullLogger<GenericCellSizer>()));
            cellCollisionDetection = new CellCollisionDetection(helper);
            bodySystemSolver = new GenericCellBodySystemSolver(cellCollisionDetection, cellSizer);
            cellGrower = new GenericCellGrower(plant, environment, bodySystemSolver, optionsService);
            descriptorService = new PlantDescriptorService();
            cellCreator = new HexagonCellCreator(cellFactory);
            gridCreator = new HexagonalCellGridFactory(cellCreator, CornCellTypeLocator.GetCornCellTypeLocator(), optionsService);
            plantPartCellCreator = new PlantPartCellCreator(gridCreator);
            internodePartFactory = new GenericInternodePartFactory(optionsService, gridCreator);
            stemPartFactory = new GenericStemPartFactory(optionsService, internodePartFactory);
            petiolePartFactory = new GenericPetiolePartFactory(cellFactory, optionsService);
            nodePartFactory = new GenericNodePartFactory(optionsService, cellFactory, stemPartFactory, petiolePartFactory);
            internodePlantPartDevelopment = new InternodePartDevelopment(optionsService, nodePartFactory, cellGrower, descriptorService, internodePartFactory);
            rootFactory = new GenericRootPartFactory(optionsService, plantPartCellCreator);
            rootPlantPartDevelopment = new RootPartDevelopment(optionsService, nodePartFactory, rootFactory, cellGrower, descriptorService);
            developer = new PlantPartDeveloper(internodePlantPartDevelopment, rootPlantPartDevelopment);
            sucroseCarrierCollection =
                new SucroseCarrierCollection(
                    new LoggerAdapter<SucroseCarrierCollection>(new NullLogger<SucroseCarrierCollection>()));
            sucroseTransporter = new SucroseTransporter(cellCollisionDetection, helper, sucroseCarrierCollection, new LoggerAdapter<FluidTransporter<Sucrose>>(new NullLogger<FluidTransporter<Sucrose>>()));
            plantGrower = new GenericPlantGrower(bodySystemSolver, developer, sucroseTransporter);
            runner = new GenericPlantRunner(plant, environment, plantGrower, new FluidsPlantCycle(plant, optionsService, sucroseCarrierCollection, new LoggerAdapter<FluidsPlantCycle>(new NullLogger<FluidsPlantCycle>())));
            RangeExtensions.Random = new Random(optionsService.Options.Simulation.RandomSeed);
        }

        [Test]
        public void Tick_WhenGivenTestPlant_ShouldIncreaseSize()
        {
            for (uint i = 0; i < 10; i++)
            {
                var snapshot = new SimulationStateSnapshot(i);

                runner.Tick(snapshot);
            }

            var stem = plant.ShootSystem.Stem;

            foreach (var cell in stem.Cells)
            {
                Assert.True(cell.Geometry.TopCenter.Y > 0, "The cell did not grow");
            }
        }

    }
}