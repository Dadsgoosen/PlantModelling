using System.Collections.Generic;
using System.Data;
using System.Numerics;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Generic;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public class GenericInternodePartFactory : IInternodePartFactory
    {
        private const int Width = 21;
        
        private const int Depth = 17;

        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly ICellGridFactory cellGridFactory;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public GenericInternodePartFactory(IPlantSimulatorOptionsService optionsService, ICellGridFactory cellGridFactory)
        {
            this.optionsService = optionsService;
            this.cellGridFactory = cellGridFactory;
        }

        public Internode CreateInternode(Vector3 center, Node node, float height, int branchCount)
        {
            bool initial = branchCount == 0;

            var cells = CreateInternodeCells(center, height, initial);

            return new GenericInternode(cells, node, branchCount);
        }

        public Internode CreateInternode(Vector3 center, float height, int branchCount)
        {
            bool initial = branchCount == 0;

            var cells = CreateInternodeCells(center, height, initial);

            return new GenericInternode(cells, branchCount);
        }

        private IEnumerable<IPlantCell> CreateInternodeCells(Vector3 center, float height, bool initial)
        {
            var width = initial ? Options.Plant.InitialStemWidth : Options.Plant.NewStemWidth.RandomNumberBetween();

            return cellGridFactory.CreateCellGrid(width, Depth, center, height);
        }
    }
}