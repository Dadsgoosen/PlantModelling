using System.Numerics;
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

        [SetUp]
        public void Setup()
        {
            face = new Mock<IFace>();
        }

        [Test]
        public void Length_PositiveCoordinates_ShouldReturnCorrectLength()
        {
            var geometry = new CellGeometry(
                new Vector3(0, 1, 2), 
                new Vector3(1, 2, 3), 
                face.Object);
            Assert.AreEqual(1.732, geometry.Length,0.0001f);
        }

        [Test]
        public void Length_NegativeCoordinates_ShouldReturnCorrectLength()
        {
            var geometry = new CellGeometry(
                new Vector3(0, -1, -2), 
                new Vector3(-1, -2, -3), 
                face.Object);
            Assert.AreEqual(1.732, geometry.Length, 0.0001f);
        }

    }
}