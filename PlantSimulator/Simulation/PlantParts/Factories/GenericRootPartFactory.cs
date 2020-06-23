using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public class GenericRootPartFactory : IRootPartFactory
    {
        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly IPlantPartCellCreator cellCreator;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public GenericRootPartFactory(IPlantSimulatorOptionsService optionsService, IPlantPartCellCreator cellCreator)
        {
            this.optionsService = optionsService;
            this.cellCreator = cellCreator;
        }

        public Root CreateRoot(Vector3 center, int branchCount)
        {
            var cells = CreateCells(center);

            return new GenericRoot(cells, branchCount);
        }

        private IEnumerable<IPlantCell> CreateCells(Vector3 center)
        {
            var cells = cellCreator.CreateCells(4, 4, center, 0f);

            return cells;
        }
    }
}