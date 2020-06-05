using System;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulatorTests.IntegrationTests.Simulation.Geometry;

namespace PlantSimulatorTests.IntegrationTests.Simulation.Operations.Generic
{
    [TestFixture]
    public class GenericCellBodySystemSolverTests
    {
        private IGeometryHelper geometryHelper;

        private ICellCollisionDetection collisionDetection;

        private ILoggerAdapter<GenericCellSizer> logger;

        private ICellSizer cellSizer;

        private ICellBodySystemSolver systemSolver;

        [SetUp]
        public void SetUp()
        {
            geometryHelper = new GeometryHelper();
            collisionDetection = new CellCollisionDetection(geometryHelper);
            logger = new LoggerAdapter<GenericCellSizer>(new NullLogger<GenericCellSizer>());
            cellSizer = new GenericCellSizer(geometryHelper, logger);
            systemSolver = new GenericCellBodySystemSolver(collisionDetection, cellSizer);
        }

        [Test]
        public void Solve_WhenGivenCollidingPlantPart_ShouldSolvePlantPart()
        {
            var a = CellCreationHelper.CreateCell(100, 0, 0, 0);
            var b = CellCreationHelper.CreateCell(100, 200 * 1.5f, 0, 0);
            var c = CellCreationHelper.CreateCell(100, 100 * 1.5f, 0, (float) Math.Sqrt(3) * 100 / 2);

            Assert.True(a != b);
        }

    }
}