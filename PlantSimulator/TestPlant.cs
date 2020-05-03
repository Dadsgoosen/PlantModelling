using System;
using System.Collections.Generic;
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
            var stem = new GenericStem(CreateStemCells());

            IShootSystem shoot = new GenericShootSystem(stem);
            IRootSystem root = new GenericRootSystem(new GenericRoot(new List<IPlantCell>(0), new List<IRoot>(0)));

            return new GenericPlant(shoot, root);
        }

        private static IEnumerable<IPlantCell> CreateStemCells()
        {
            var cells = new List<IPlantCell>();

            const int radius = 60;

            for (int i = 0; i < 6; i++)
            {
                cells.Add(CreateCell(radius, i * radius,0,0));
            }

            return cells;
        }

        private static IPlantCell CreateCell(float radius, float x, float y, float z)
        {
            var top = new Vertex(x, y, z);
            var bottom = new Vertex(x, y, z);
            var face = CreateFace(top, radius);

            return new XylemCell(new CellGeometry(top, bottom, face), new IPlantCell[0], new Vacuole(), new CellWall());
        }

        private static IFace CreateFace(IVertex center, float radius)
        {
            IVertex[] vertices = new IVertex[6];

            const int sides = 6;
            const int degrees = 360 / 6;

            for (int i = 0; i < sides; i++)
            {
                vertices[i] = new Vertex
                {
                    X = center.X + radius * (float) Math.Cos(i * degrees * Math.PI / 180f),
                    Y = center.Y,
                    Z = center.Z + radius * (float) Math.Sin(i * degrees * Math.PI / 180f),
                };
            }

            return new Face(vertices);
        }
    }
}