using Moq;
using NUnit.Framework;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulatorTests.UnitTests.Simulation.Geometry
{
    [TestFixture]
    public class CellGeometryTests
    {
        private Mock<IFace> face;

        private Mock<IVertex> top;

        private Mock<IVertex> bottom;

        private ICellGeometry geometry;

        [SetUp]
        public void Setup()
        {
            face = new Mock<IFace>();
            top = new Mock<IVertex>();
            bottom = new Mock<IVertex>();
            geometry = new CellGeometry(top.Object, bottom.Object, face.Object);
        }

        [Test]
        public void Length_PositiveCoordinates_ShouldReturnCorrectLength()
        {
            top.SetupGet(v => v.X).Returns(0);
            top.SetupGet(v => v.Y).Returns(1);
            top.SetupGet(v => v.Z).Returns(2);

            bottom.SetupGet(v => v.X).Returns(1);
            bottom.SetupGet(v => v.Y).Returns(2);
            bottom.SetupGet(v => v.Z).Returns(3);

            Assert.AreEqual(1.732, geometry.Length,0.0001f);
        }

        [Test]
        public void Length_NegativeCoordinates_ShouldReturnCorrectLength()
        {
            top.SetupGet(v => v.X).Returns(0);
            top.SetupGet(v => v.Y).Returns(-1);
            top.SetupGet(v => v.Z).Returns(-2);

            bottom.SetupGet(v => v.X).Returns(-1);
            bottom.SetupGet(v => v.Y).Returns(-2);
            bottom.SetupGet(v => v.Z).Returns(-3);

            Assert.AreEqual(1.732, geometry.Length, 0.0001f);
        }

    }
}