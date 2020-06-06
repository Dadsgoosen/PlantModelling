using System;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Cells.Factories
{
    /// <summary>
    /// Plant Cell Factory that creates non-specialized generic plant cells
    /// </summary>
    public class GenericCellFactory : ICellFactory
    {
        
        /// <summary>
        /// Helper method to instantiate a new plant cell type based on the provided type
        /// </summary>
        /// <param name="type">The type of cell to new</param>
        /// <param name="geometry">The new geometry</param>
        /// <param name="vacuole">The vacuole organ</param>
        /// <param name="wall">The cell wall</param>
        /// <param name="neighbors">The neighboring plant cells</param>
        /// <returns>New cell of type <see cref="type"/> with provided parameters</returns>
        public IPlantCell CreateCell(PlantCellType type, ICellGeometry geometry, IVacuole vacuole, ICellWall wall)
        {
            return InstantiateCell(type, geometry, vacuole, wall);
        }

        /// <summary>
        /// Helper method to instantiate a new plant cell type based on the provided type
        /// </summary>
        /// <param name="type">The type of cell to new</param>
        /// <param name="geometry">The new geometry</param>
        /// <param name="vacuole">The vacuole organ</param>
        /// <param name="wall">The cell wall</param>
        /// <param name="neighbors">The neighboring plant cells</param>
        /// <returns>New cell of type <see cref="type"/> with provided parameters</returns>
        private IPlantCell InstantiateCell(PlantCellType type, ICellGeometry geometry, IVacuole vacuole, ICellWall wall)
        {
            switch (type)
            {
                case PlantCellType.Xylem:
                    return new XylemCell(geometry, vacuole, wall);
                case PlantCellType.Phloem:
                    return new PhloemCell(geometry, vacuole, wall);
                case PlantCellType.Sclerenchyma:
                    return new SclerenchymaCell(geometry, vacuole, wall);
                case PlantCellType.Collenchyma:
                    return new CollenchymaCell(geometry, vacuole, wall);
                case PlantCellType.Parenchyma:
                    return new ParenchymaCell(geometry, vacuole, wall);
                default:
                    throw new ArgumentOutOfRangeException(nameof(type), type, "Invalid Plant Cell Type");
            }
        }
    }
}