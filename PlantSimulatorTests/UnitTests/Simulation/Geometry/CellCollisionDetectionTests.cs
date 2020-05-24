using System;
using System.Numerics;
using NUnit.Framework;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulatorTests.UnitTests.Simulation.Geometry
{
    [TestFixture]
    public class CellCollisionDetectionTests
    {

        private ICellCollisionDetection cellCollisionDetection;

        [SetUp]
        public void SetUp()
        {
            cellCollisionDetection = new CellCollisionDetection();
        }

        [Test]
        public void IsColliding_WhenGivenACollidingCell_ShouldReturnTrue()
        {
            IPlantCell a = CreateCell(30, 0, 0, 0);
            IPlantCell b = CreateCell(30, 10, 0, 0);

            Assert.True(cellCollisionDetection.IsColliding(a, b));
        }

        [Test]
        public void IsColliding_WhenGivenNoneCollidingCell_ShouldReturnFalse()
        {
            IPlantCell a = CreateCell(30, 0, 0, 0);
            IPlantCell b = CreateCell(5, 15, 0, 0);

            Assert.False(cellCollisionDetection.IsColliding(a, b));
        }

        [Test]
        public void IsColliding_WhenGivenCellOnBorder_ShouldReturnTrue()
        {
            IPlantCell a = CreateCell(30, 0, 0, 0, true);
            IPlantCell b = CreateCell(30, 30, 0, 0, true);

            Assert.True(cellCollisionDetection.IsColliding(a, b));
        }

        [Test]
        public void AreNeighbor_WhenGivenNeighboringCell_ShouldReturnTrue()
        {
            var a = CreateCell(10, 0, 0, 0, true);
            var b = CreateCell(10, 10, 0, 0, true);
            Assert.True(cellCollisionDetection.AreNeighbors(a, b));
        }

        [Test]
        public void AreNeighbor_WhenGivenNoneNeighboringCell_ShouldReturnFalse()
        {
            var a = CreateCell(10, 0, 0, 0, true);
            var b = CreateCell(10, 30, 0, 20, true);
            Assert.False(cellCollisionDetection.AreNeighbors(a, b));
        }

        [Test]
        public void GetNeighbors_WhenGiveNeighboringCell_ShouldReturnNoneEmptyList()
        {
            var a = CreateCell(10, 0, 0, 0, true);
            var b = CreateCell(10, 10, 0, 5, true);

            var neighbors = cellCollisionDetection.GetNeighbors(a, new[] {b});
            
            Assert.IsNotEmpty(neighbors);
            Assert.AreEqual(1, neighbors.Count);
            Assert.IsNotNull(neighbors[0]);
            Assert.AreEqual(b, neighbors[0]);
        }

        [Test]
        public void GetNeighbors_WhenGivenNoneNeighboringCell_ShouldReturnEmptyList()
        {
            var a = CreateCell(10, 0, 0, 0, true);
            var b = CreateCell(10, 30, 0, 20, true);
            Assert.IsEmpty(cellCollisionDetection.GetNeighbors(a, new[] {b}));
        }

        [Test]
        public void GetClosestPoint_WhenGivenAPointInsideFace_FindTheNearestPointOutside()
        {
            var cell = CreateCell(10, 0, 0, 0, true);
            var face = cell.Geometry.Face;
            
            var point = new Vector2(2.5F, 0);
            var nearest = cellCollisionDetection.GetClosestPoint(point, face.Points);

            Assert.AreEqual(5, nearest.X, 0.0001);
            Assert.AreEqual(point.Y, nearest.Y, 0.0001);
            Assert.AreEqual(0, nearest.Y, 0.0001);
        }


        private static IPlantCell CreateCell(float radius, float x, float y, float z, bool square = false)
        {
            var top = new Vector3(x, y + 10, z);
            var bottom = new Vector3(x, y, z);
            var face = square ? CreateSquare(top, radius) : CreateFace(top, radius);

            return new XylemCell(new CellGeometry(top, bottom, face), new IPlantCell[0], new Vacuole(), new CellWall());
        }

        private static IFace CreateFace(Vector3 center, float radius)
        {
            Vector2[] vertices = new Vector2[6];

            const int sides = 6;
            const int degrees = 360 / 6;

            for (int i = 0; i < sides; i++)
            {
                vertices[i] = new Vector2
                {
                    X = center.X + radius * (float)Math.Cos(i * degrees * Math.PI / 180f),
                    Y = center.Z + radius * (float)Math.Sin(i * degrees * Math.PI / 180f)
                };
            }

            return new Face(vertices);
        }

        private static IFace CreateSquare(Vector3 center, float size)
        {
            Vector2[] vertices = {
                new Vector2(center.X + (size / 2), (-size / 2) + center.Z),
                new Vector2(center.X + (-size / 2), (-size / 2) + center.Z),
                new Vector2(center.X + (-size / 2), (size / 2) + center.Z),
                new Vector2(center.X + (size / 2), (size / 2) + center.Z)
            };

            return new Face(vertices);
        }

    }
}