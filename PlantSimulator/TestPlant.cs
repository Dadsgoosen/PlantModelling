using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;

namespace PlantSimulator
{
    public static class TestPlant
    {
        public static IPlant CreatePlant()
        {
            var internode = new GenericInternode(CreateStemCells(), 0);

            var stem = new GenericStem(internode, 0);

            IShootSystem shoot = new GenericShootSystem(stem);
            IRootSystem root = new GenericRootSystem(new GenericRoot(new List<IPlantCell>(0), new List<Root>(0), 0));

            return new GenericPlant(shoot, root);
        }

        private static IEnumerable<IPlantCell> CreateStemCells()
        {
            const int radius = 60;

            const float w = 2 * radius;

            float h = (float) Math.Sqrt(3) * radius;

            const float horizontalSpacing = w * 0.75F;

            return new[]
            {
                CreateCell(radius, 0, 0, 0),
                CreateCell(radius, 0, 0, h),
                CreateCell(radius, horizontalSpacing, 0, h * 0.5F),
                CreateCell(radius, horizontalSpacing, 0, h * -0.5F),
                CreateCell(radius, 0, 0, -h),
                CreateCell(radius, -horizontalSpacing, 0, h * 0.5F),
                CreateCell(radius, -horizontalSpacing, 0, h * -0.5F),
            };
        }

        private static IPlantCell CreateCell(float radius, float x, float y, float z)
        {
            var top = new Vector3(x, y, z);
            var bottom = new Vector3(x, y, z);
            var face = CreateFace(top, radius);

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
                    X = center.X + radius * (float) Math.Cos(i * degrees * Math.PI / 180f),
                    Y = center.Z + radius * (float) Math.Sin(i * degrees * Math.PI / 180f),
                };
            }

            return new Face(vertices);
        }
    }
}