using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class CellCreatorHelper : ICellCreatorHelper
    {
        private float w;

        private float h;

        private float r;

        public IList<IPlantCell> CreateCell()
        {
            return CreateCells(10);
        }

        private IList<IPlantCell> CreateCells(float radius)
        {
            r = radius;
            w = 2 * r;
            h = (float)Math.Sqrt(3) * r;

            return new List<IPlantCell>(CreateRowColumns());
        }

        private IEnumerable<IPlantCell> CreateRowColumns()
        {
            var cells = new List<IPlantCell>();

            for (int c = 1; c < 10; c++)
            {

            }

            return cells;
        }

        private IPlantCell[] CreateColumn(int count, int column)
        {
            IPlantCell[] cells = new IPlantCell[count];

            int half = count / 2;

            float x = column * (w * 1.5f);

            float z = 0;

            cells[0] = CreatePlantCell(GetCellType(0, column), r, x, z);

            for (int i = 0; i < half; i++)
            {

            }

            return cells;
        }

        private PlantCellType GetCellType(int row, int column)
        {
            return PlantCellType.Parenchyma;
        }

        private IPlantCell CreatePlantCell(PlantCellType type, float radius, float x, float z)
        {
            float y = 0;

            var top = new Vector3(x, y + 10, z);
            var bottom = new Vector3(x, y, z);
            var face = CreateCellFace(radius, top);

            switch (type)
            {
                case PlantCellType.Xylem:
                    return new XylemCell(new CellGeometry(top, bottom, face), new Vacuole(), new CellWall());
                case PlantCellType.Phloem:
                    return new PhloemCell(new CellGeometry(top, bottom, face), new Vacuole(), new CellWall());
                case PlantCellType.Sclerenchyma:
                    return new SclerenchymaCell(new CellGeometry(top, bottom, face), new Vacuole(), new CellWall());
                case PlantCellType.Collenchyma:
                    return new CollenchymaCell(new CellGeometry(top, bottom, face), new Vacuole(), new CellWall());
                case PlantCellType.Parenchyma:
                    return new ParenchymaCell(new CellGeometry(top, bottom, face), new Vacuole(), new CellWall());
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Unknown cell type was given to the cell creator");
            }
        }

        private IFace CreateCellFace(float radius, Vector3 center)
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