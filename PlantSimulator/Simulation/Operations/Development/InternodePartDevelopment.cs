using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells;
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

        private Node CreateNewUpperNode(Internode internode, IPlantPartDescriptor descriptor)
        {
            var node = new GenericNode(internode);

            var newLower = new GenericInternode(cellCreator.CreateCell(10), node);

            node.UpperInternode = newLower;

            return node;
        }

        private Node SpawnNode(Internode lowerInternode)
        {
            int petioleCount = Options.Plant.PetiolesPerNode.RandomNumberBetween();

            Petiole[] petioles = new Petiole[petioleCount];
            
            int stemCount = Options.Plant.StemsPerNode.RandomNumberBetween();
            Stem[] stems = new Stem[stemCount];

            if (petioleCount > 0)
            {
                for (int i = 0; i < petioleCount; i++)
                {
                    petioles[i] = CreatePetiole();
                }
            }

            if (stemCount > 0)
            {
                for (int i = 0; i < petioleCount; i++)
                {
                    stems[i] = CreateStem();
                }
            }

            var node = new GenericNode(lowerInternode);


            return 
        }

        private Stem CreateStem()
        {

        }

        private Petiole CreatePetiole()
        {
            var petioleWidth = Options.Plant.NewPetioleWidth;
            var cells = cellCreator.CreateCell(petioleWidth.RandomNumberBetween());
            return new GenericPetiole(cells);
        }
    }
}