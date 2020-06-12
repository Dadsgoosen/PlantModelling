using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.PlantParts.Corn;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class CellCreatorHelper : ICellCreatorHelper
    {
        /// <summary>
        /// The width of the plant cell
        /// </summary>
        private float w;

        /// <summary>
        /// The depth of the plant cell
        /// </summary>
        private float h;

        /// <summary>
        /// The radius/size of the cells
        /// </summary>
        private float r;


        private ICellTypeLocator cellTypeLocator;

        public IList<IPlantCell> CreateCell()
        {
            return CreateCells(10);
        }

        private IList<IPlantCell> CreateCells(float radius)
        {
            r = radius;
            w = 2 * r;
            h = (float)Math.Sqrt(3) * r;
            cellTypeLocator = CornCellTypes.GetCornCellTypeLocator();

            return new List<IPlantCell>(CreateRowColumns());
        }

        private IEnumerable<IPlantCell> CreateRowColumns()
        {
            var cells = new List<IPlantCell>();

            int columns = 21;
            int max = 17;
            int end = 7;
            int current = max;

            for (int c = 0; c < 11; c++)
            {
                cells.AddRange(CreateNewCells(current, c));
                current--;
            }

            current = max - 1;

            for (int c = -1; c >= -10; c--)
            {
                cells.AddRange(CreateNewCells(current, c));
                current--;
            }

            return cells;
        }

        private IEnumerable<IPlantCell> CreateNewCells(int current, int column)
        {
            return column % 2 == 0 ? CreateEven(current, column) : CreateOdd(current, column);
        }

        private IEnumerable<IPlantCell> CreateOdd(int total, int column)
        {
            var cells = new List<IPlantCell>(total);

            int half = (int)Math.Floor(total / 2m);

            float x = ComputeX(column);
            float z = r;

            for (int i = 0; i < half; i++)
            {
                cells.Add(CreatePlantCell(GetCellType(i, column), r, x, z));
                z += h;
            }

            z = -r;

            for (int i = -1; i >= -half; i--)
            {

                cells.Add(CreatePlantCell(GetCellType(i, column), r, x, z));
                z -= h;
            }

            return cells;
        }

        private IEnumerable<IPlantCell> CreateEven(int total, int column)
        {
            var cells = new List<IPlantCell>(total);

            int half = (int) Math.Floor(total / 2m);

            float x = ComputeX(column);
            float z = ComputeZ(0);

            cells.Add(CreatePlantCell(GetCellType(0, column), r, x, z));

            for (int i = 1; i <= half; i++)
            {
                z = ComputeZ(i);
                cells.Add(CreatePlantCell(GetCellType(i, column), r, x, z));
            }

            for (int i = -1; i >=-half; i--)
            {
                z = ComputeZ(i);
                cells.Add(CreatePlantCell(GetCellType(i, column), r, x, z));
            }

            return cells;
        }


        private PlantCellType GetCellType(int row, int column)
        {
            return cellTypeLocator.Get(row, column);
        }

        private float ComputeZ(int row)
        {
            return row * h;
        }

        private float ComputeX(int column)
        {
            return column * w;
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