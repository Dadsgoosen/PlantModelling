using System.Linq;
using NUnit.Framework;
using PlantSimulator;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;

namespace PlantSimulatorTests.IntegrationTests.Simulation.Operations.Generic
{
    [TestFixture]
    public class GenericPlantRunnerIntegrationTests
    {
        private SimulationEnvironment environment;

        private IPlant plant;

        private ICellFactory cellFactory;

        private ICellCollisionDetection cellCollisionDetection;

        private ICellBodySystemSolver bodySystemSolver;

        private ICellDivider divider;

        private ICellGrower cellGrower;

        private IPlantGrower plantGrower;

        private IPlantRunner runner;

        private IGeometryHelper helper;

        [SetUp]
        public void Setup()
        {
            environment = new SimulationEnvironment {LightPosition = new Vertex(0, 10000, 0), Temperature = 22};
            plant = TestPlant.CreatePlant();
            cellFactory = new GenericCellFactory();
            divider = new GenericCellDivider(cellFactory);
            helper = new GeometryHelper();
            cellCollisionDetection = new CellCollisionDetection(helper);
            bodySystemSolver = new GenericCellBodySystemSolver(cellCollisionDetection);
            cellGrower = new GenericCellGrower(plant, environment, bodySystemSolver);
            plantGrower = new GenericPlantGrower(cellGrower, environment);
            runner = new GenericPlantRunner(plant, environment, plantGrower);
        }

        [Test]
        public void Tick_WhenGivenTestPlant_ShouldIncreaseSize()
        {
            for (uint i = 0; i < 10; i++)
            {
                var snapshot = new SimulationStateSnapshot(i);

                runner.Tick(snapshot);
            }

            var stem = (IPlantPart) plant.ShootSystem.Stem;

            var area = stem.Cells.First().Geometry.Face.Area;

            foreach (var cell in stem.Cells)
            {
                Assert.True(cell.Geometry.TopCenter.Y > 0, "The cell did not grow");
            }
        }

    }
}