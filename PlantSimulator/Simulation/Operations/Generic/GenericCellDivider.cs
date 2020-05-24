using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellDivider : ICellDivider
    {
        private readonly ICellFactory cellFactory;

        public GenericCellDivider(ICellFactory cellFactory)
        {
            this.cellFactory = cellFactory;
        }

        public bool ShouldDivide(IPlantCell cell)
        {
            return false;
        }

        public IPlantCell[] Divide(IPlantCell cell)
        {
            ICellGeometry geo = cell.Geometry;

            Vector3 halfPoint = GetCellHalfWay(geo);

            ICellGeometry topCellGeometry = new CellGeometry(geo.TopCenter, halfPoint, geo.Face);
            ICellGeometry bottomCellGeometry = new CellGeometry(halfPoint, geo.BottomCenter, geo.Face);

            IPlantCell[] cells = new IPlantCell[2];

            cells[0] = CreatePlantCell(cell.CellType, topCellGeometry, cell.Vacuole, cell.CellWall,
                cell.Neighbors);
            cells[1] = CreatePlantCell(cell.CellType, bottomCellGeometry, cell.Vacuole, cell.CellWall,
                cell.Neighbors);

            return cells;
        }

        private IPlantCell CreatePlantCell(PlantCellType type, ICellGeometry geometry, IVacuole vacuole,
            ICellWall cellWall, IPlantCell[] neighbors)
        {
            return cellFactory.CreateCell(type, geometry, vacuole, cellWall, neighbors);
        }

        private static Vector3 GetCellHalfWay(ICellGeometry geometry)
        {
            var top = geometry.TopCenter;
            var bottom = geometry.BottomCenter;

            return new Vector3
            {
                X = (top.X + bottom.X) / 2,
                Y = (top.Y + bottom.Y) / 2,
                Z = (top.Z + bottom.Z) / 2
            };
        }
    }
}