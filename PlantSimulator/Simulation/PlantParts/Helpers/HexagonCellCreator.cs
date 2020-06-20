using System;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class HexagonCellCreator : ISingularCellCreator
    {
        private readonly ICellFactory cellFactory;

        public HexagonCellCreator(ICellFactory cellFactory)
        {
            this.cellFactory = cellFactory;
        }

        public IPlantCell CreateCell(PlantCellType type, Vector3 center, float radius, float height)
        {
            var geometry = CreateCellGeometry(center, radius, height);

            var vacuole = new Vacuole();
            
            var wall = new CellWall();

            return cellFactory.CreateCell(type, geometry, vacuole, wall);
        }

        private static ICellGeometry CreateCellGeometry(Vector3 center, float radius, float height)
        {
            var top = center + new Vector3(0, height, 0);
            
            var bottom = center;
            
            var face = CreateCellFace(radius, center);

            return new CellGeometry(top, bottom, face);
        }

        private static IFace CreateCellFace(float radius, Vector3 center)
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
    }
}