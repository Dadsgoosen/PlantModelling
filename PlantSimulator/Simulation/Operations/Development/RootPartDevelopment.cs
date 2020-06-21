using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Operations.Development
{
    public class RootPartDevelopment : IPlantPartDevelopment<Root>
    {
        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly INodePartFactory nodePartFactory;

        private readonly ICellGrower cellGrower;

        private readonly IPlantDescriptorService plantDescriptorService;

        public RootPartDevelopment(IPlantSimulatorOptionsService optionsService, INodePartFactory nodePartFactory, ICellGrower cellGrower, IPlantDescriptorService plantDescriptorService)
        {
            this.optionsService = optionsService;
            this.nodePartFactory = nodePartFactory;
            this.cellGrower = cellGrower;
            this.plantDescriptorService = plantDescriptorService;
        }

        public void Develop(Root plantPart, SimulationStateSnapshot snapshot)
        {
            var descriptor = plantDescriptorService.Describe(plantPart);

            foreach (var cell in plantPart.Cells)
            {
                cellGrower.GrowRootCell(cell, plantPart, snapshot);
            }
        }
    }
}