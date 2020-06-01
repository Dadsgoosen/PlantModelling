using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulatorTests.IntegrationTests.Simulation.Geometry;

namespace PlantSimulatorTests.IntegrationTests.Simulation.Operations.Generic
{
    [TestFixture]
    public class GenericCellSizerTests
    {
        private IGeometryHelper geometryHelper;

        private ICellSizer cellSizer;

        private ICellCollisionDetection collisionDetection;

        private ILoggerAdapter<GenericCellSizer> logger;

        [SetUp]
        public void SetUp()
        {
            geometryHelper = new GeometryHelper();
            collisionDetection = new CellCollisionDetection(geometryHelper);
            logger = new LoggerAdapter<GenericCellSizer>(new Logger<GenericCellSizer>(new NullLoggerFactory()));
            cellSizer = new GenericCellSizer(geometryHelper, logger);
        }

        [Test]
        public void Test()
        {
            var a = CellCreationHelper.CreateCell(20, 42, 0, 4);
            var b = CellCreationHelper.CreateCell(20, 20, 0, 17);

            var colliding = collisionDetection.Colliding(a, b, true);

            Assert.True(colliding);

            colliding = collisionDetection.Colliding(a, b, true);

            Assert.False(colliding);
        }

    }
}