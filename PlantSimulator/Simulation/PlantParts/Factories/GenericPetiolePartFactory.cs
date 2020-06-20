using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.Options;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public class GenericPetiolePartFactory : IPetiolePartFactory
    {
        private readonly IPlantSimulatorOptionsService optionsService;

        private IPlantSimulatorOptions Options => optionsService.Options;

        private readonly ICellFactory cellCreator;

        public GenericPetiolePartFactory(ICellFactory cellCreator, IPlantSimulatorOptionsService optionsService)
        {
            this.cellCreator = cellCreator;
            this.optionsService = optionsService;
        }

        public Petiole CreatePetiole(Vector3 center, Vector3 direction)
        {
            var cells = CreateCells(center, direction);

            var leafs = new GenericLeaf[0];

            return new GenericPetiole(cells, leafs);
        }

        private IEnumerable<IPlantCell> CreateCells(Vector3 center, Vector3 direction)
        {
            IPlantCell[] cells = 
            {
                CreateCell(center, direction)
            };

            return cells;
        }

        private IPlantCell CreateCell(Vector3 center, Vector3 direction)
        {
            var geometry = CreateCellGeometry(center, direction);

            var vacuole = new Vacuole();

            var cellWall = new CellWall();

            return cellCreator.CreateCell(PlantCellType.Parenchyma, geometry, vacuole, cellWall);
        }

        private static ICellGeometry CreateCellGeometry(Vector3 center, Vector3 direction)
        {
            var top = center + direction;
            var bottom = center;
            var face = new Face(new Vector2[0]);
            return new CellGeometry(top, bottom, face);
        }
    }
}