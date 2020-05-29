using NUnit.Framework;
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

        [SetUp]
        public void SetUp()
        {
            geometryHelper = new GeometryHelper();
            collisionDetection = new CellCollisionDetection(geometryHelper);
            cellSizer = new GenericCellSizer(geometryHelper);
        }

        [Test]
        public void Test()
        {
            var a = CellCreationHelper.CreateCell(20, 42, 0, 4);
            var b = CellCreationHelper.CreateCell(20, 20, 0, 17);

            var colliding = collisionDetection.Colliding(a, b, true);

            Assert.True(colliding);

            cellSizer.Resize(a, b);

            colliding = collisionDetection.Colliding(a, b, true);

            Assert.False(colliding);
        }

    }
}