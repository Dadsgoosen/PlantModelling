using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Operations.Development
{
    public class InternodePartDevelopment : IPlantPartDevelopment<Internode>
    {
        private readonly ICellCreatorHelper cellCreator;

        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly IPlantDescriptorService plantDescriptorService;

        public InternodePartDevelopment(ICellCreatorHelper cellCreator, IPlantSimulatorOptionsService optionsService, IPlantDescriptorService plantDescriptorService)
        {
            this.cellCreator = cellCreator;
            this.optionsService = optionsService;
            this.plantDescriptorService = plantDescriptorService;
        }

        private IPlantSimulatorOptions Options => optionsService.Options;

        public void Develop(Internode plantPart, SimulationStateSnapshot snapshot)
        {
            var description = plantDescriptorService.Describe(plantPart);

            if (ShouldAddNewNode(plantPart, snapshot, description.Height))
            {
                plantPart.UpperNode = CreateNewUpperNode(plantPart);
            }
        }

        private bool ShouldAddNewNode(Internode internode, SimulationStateSnapshot snapshot, float height)
        {
            if (internode.HasUpperNode())
            {
                return false;
            }

            var maxLength = Options.Plant.MaxInternodeLength.RandomNumberBetween();

            if (height >= maxLength)
            {
                return true;
            }

            uint due = (uint)Options.Plant.NewNodeTickCount.RandomNumberBetween();

            if (due >= snapshot.CurrentTime)
            {
                return true;
            }

            return false;
        }

        private Node CreateNewUpperNode(Internode internode)
        {
            var node = new GenericNode(internode);

            var newLower = new GenericInternode(cellCreator.CreateCell(10), node);

            node.UpperInternode = newLower;

            return node;
        }
    }
}