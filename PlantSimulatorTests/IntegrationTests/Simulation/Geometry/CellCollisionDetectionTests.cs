using System;
using System.Collections;
using System.Numerics;
using NUnit.Framework;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulatorTests.IntegrationTests.Simulation.Geometry
{
    [TestFixture]
    public class CellCollisionDetectionTests
    {
        private IGeometryHelper helper;

        private ICellCollisionDetection cellCollisionDetection;

        [SetUp]
        public void SetUp()
        {
            helper = new GeometryHelper();
            cellCollisionDetection = new CellCollisionDetection(helper);
        }

        [TestCaseSource(typeof(NeighboringCellDataSource))]
        public bool Neighbors_WhenGivenTwoCells_ReturnNeighboringStatus(IPlantCell a, IPlantCell b, bool allow)
        {
            return cellCollisionDetection.Neighbors(a, b, allow);
        }

        [TestCaseSource(typeof(CollidingCellDataSource))]
        public bool Colliding_WhenGivenTwoCells_ReturnCollidingStatus(IPlantCell a, IPlantCell b, bool line)
        {
            return cellCollisionDetection.Colliding(a, b, line);
        }
    }

    internal class NeighboringCellDataSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 2.5F, 0, 2.5F, true), false).Returns(false);
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 2.5F, 0, 2.5F, true), true).Returns(true);
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 25, 0, 25, true), true).Returns(false);
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 0, 20, 0, true), true).Returns(false);
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 0, 20, 0, true), false).Returns(false);
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 0, 10, 0, true), false).Returns(true);
            yield return new TestCaseData(CellCreationHelper.CreateCell(10, 0, 0, 0, true), CellCreationHelper.CreateCell(10, 0, 10, 0, true), true).Returns(true);
        }
    }

    internal class CollidingCellDataSource : IEnumerable
    {
        public IEnumerator GetEnumerator()
        {
            yield return CreateTestCase(
                CellCreationHelper.CreateCell(10, 0, 0, 0, true), 
                CellCreationHelper.CreateCell(10, 2.5f, 0, 0, true), 
                false,
                "Colliding inside and is not allowed on line", 
                true);

            yield return CreateTestCase(
                CellCreationHelper.CreateCell(10, 0, 0, 0),
                CellCreationHelper.CreateCell(10, 0, 0, (float) Math.Sqrt(3) * 10),
                false,
                "Colliding on line and is not allowed on line", 
                true);

            yield return CreateTestCase(
                CellCreationHelper.CreateCell(10, 0, 0, 0),
                CellCreationHelper.CreateCell(10, 0, 0, (float)Math.Sqrt(3) * 10),
                true,
                "Colliding on line and is allowed on line",
                false);


            yield return CreateTestCase(
                CellCreationHelper.CreateCell(10, 0, 0, 0),
                CellCreationHelper.CreateCell(10, 0, 0, (float)Math.Sqrt(3) * 10 + 1),
                true,
                "Not Colliding and is allowed on line",
                false);

            yield return CreateTestCase(
                CellCreationHelper.CreateCell(10, 0, 0, 0),
                CellCreationHelper.CreateCell(10, 0, 0, (float)Math.Sqrt(3) * 10 + 1),
                false,
                "Not Colliding and is not allowed on line",
                false);
        }

        private TestCaseData CreateTestCase(IPlantCell a, IPlantCell b, bool allowed, string name, bool returns)
        {
            return new TestCaseData(a, b, allowed).Returns(returns).SetName(name);
        }
    }

    internal static class CellCreationHelper
    {
        public static IPlantCell CreateCell(float radius, float x, float y, float z, bool square = false)
        {
            var top = new Vector3(x, y + 10, z);
            var bottom = new Vector3(x, y, z);
            var face = square ? CreateSquare(top, radius) : CreateFace(top, radius);

            return new XylemCell(new CellGeometry(top, bottom, face), new Vacuole(), new CellWall());
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