using System;
using System.Linq;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PlantSimulator;
using PlantSimulator.Helpers;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.Operations.Development;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Corn;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;
using PlantSimulatorTests.UnitTests.Simulation.PlantParts.Helpers;

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

        private HexagonCellCreator cellCreator;

        private HexagonalCellGridFactory gridCreator;

        private PlantPartCellCreator plantPartCellCreator;

        private GenericNodePartFactory nodePartFactory;
        
        private GenericInternodePartFactory internodePartFactory;

        private GenericStemPartFactory stemPartFactory;

        private GenericPetiolePartFactory petiolePartFactory;

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
            cellGrower = new GenericCellGrower(plant, environment, bodySystemSolver);
            descriptorService = new PlantDescriptorService();
            cellCreator = new HexagonCellCreator(cellFactory);
            gridCreator = new HexagonalCellGridFactory(cellCreator, CornCellTypeLocator.GetCornCellTypeLocator(), optionsService);
            plantPartCellCreator = new PlantPartCellCreator(gridCreator);
            internodePartFactory = new GenericInternodePartFactory(optionsService, gridCreator);
            stemPartFactory = new GenericStemPartFactory(optionsService, internodePartFactory);
            petiolePartFactory = new GenericPetiolePartFactory(cellFactory, optionsService);
            nodePartFactory = new GenericNodePartFactory(optionsService, plantPartCellCreator, stemPartFactory, petiolePartFactory);
            internodePlantPartDevelopment = new InternodePartDevelopment(optionsService, nodePartFactory, cellGrower, descriptorService);
            developer = new PlantPartDeveloper(internodePlantPartDevelopment);
            plantGrower = new GenericPlantGrower(cellGrower, developer);
            runner = new GenericPlantRunner(plant, environment, plantGrower);
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