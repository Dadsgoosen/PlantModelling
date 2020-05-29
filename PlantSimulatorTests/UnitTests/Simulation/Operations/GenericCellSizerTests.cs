using System;
using System.Numerics;
using NUnit.Framework;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;

namespace PlantSimulatorTests.UnitTests.Simulation.Operations
{
    [TestFixture]
    public class GenericCellSizerTests
    {
        private ICellCollisionDetection collisionDetection;

        private ICellSizer cellSizer;

        private IGeometryHelper helper;

        [SetUp]
        public void SetUp()
        {
            helper = new GeometryHelper();
            cellSizer = new GenericCellSizer(helper);
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