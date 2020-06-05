using System.Numerics;
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
        public void ResizeWidth_WhenGivenFaceCollidingCell_ShouldNotBeCollidingAfterResize()
        {
            var a = CellCreationHelper.CreateCell(10, 0, 0, 0, true);
            var b = CellCreationHelper.CreateCell(10, 2.5f, 0, 2.5f, true);

            Assert.True(collisionDetection.Colliding(a, b, true), "Cells does not collide from the start");

            cellSizer.ResizeWidth(a, b);

            Assert.False(collisionDetection.Colliding(a, b, true), "Cells still collide after resizing");
        }

        [Test]
        public void ResizeWidth_WhenGivenFaceNotCollidingCell_ShouldNotBeChanged()
        {
            var a = CellCreationHelper.CreateCell(10, 0, 0, 0, true);
            var b = CellCreationHelper.CreateCell(10, 15, 0, 15, true);

            Assert.False(collisionDetection.Colliding(a, b, true), "Cells does collide from the start");

            cellSizer.ResizeWidth(a, b);

            Assert.False(collisionDetection.Colliding(a, b, true), "Cells collide after resizing");

            Assert.AreEqual(a.Geometry.BottomCenter, new Vector3(0, 0, 0));
            Assert.AreEqual(b.Geometry.BottomCenter, new Vector3(15, 0, 15));
        }

        [Test]
        public void ResizeWHeight_WhenGivenCollidingHeight_ShouldBeChanged()
        {
            var a = CellCreationHelper.CreateCell(10, 0, 5, 0, true);
            var b = CellCreationHelper.CreateCell(10, 15, 0, 15, true);

            Assert.AreNotEqual(a.Geometry.BottomCenter.Y, b.Geometry.TopCenter.Y);

            cellSizer.ResizeHeight(a, b);

            Assert.AreEqual(a.Geometry.BottomCenter.Y, b.Geometry.TopCenter.Y, 0.00001);
        }

        [Test]
        public void ResizeWHeight_WhenGivenNoneCollidingHeight_ShouldNotBeChanged()
        {
            var a = CellCreationHelper.CreateCell(10, 0, 11, 0, true);
            var b = CellCreationHelper.CreateCell(10, 15, 0, 15, true);

            cellSizer.ResizeHeight(a, b);

            Assert.AreEqual(11, a.Geometry.BottomCenter.Y, 0.00001);
            Assert.AreEqual(21, a.Geometry.TopCenter.Y, 0.00001);
        }
    }
}