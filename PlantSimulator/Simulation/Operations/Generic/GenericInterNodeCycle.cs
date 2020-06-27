using System;
using System.Runtime.CompilerServices;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericInterNodeCycle : IInterNodeCycle
    {
        private readonly IPlantPartCellCreator cellCreator;

        private readonly IPlantSimulatorOptionsService optionsService;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public GenericInterNodeCycle(IPlantPartCellCreator cellCreator, IPlantSimulatorOptionsService optionsService)
        {
            this.cellCreator = cellCreator;
            this.optionsService = optionsService;
        }

        public void Cycle(Internode internode, SimulationStateSnapshot stateSnapshot, float height)
        {
            if (ShouldAddNewNode(internode, stateSnapshot, height))
            {
                internode.UpperNode = CreateNewUpperNode(internode);
            }
        }

        private bool ShouldAddNewNode(Internode internode, SimulationStateSnapshot snapshot, float height)
        {
            if (internode.HasUpperNode()) {
                return false;
            }

            var maxLength = Options.Plant.MaxInternodeLength.RandomNumberBetween();

            if (height >= maxLength)
            {
                return true;
            }

            uint due = (uint) Options.Plant.NewNodeTickCount.RandomNumberBetween();

            if (due >= snapshot.CurrentTime)
            {
                return true;
            }

            return false;
        }

        private Node CreateNewUpperNode(Internode internode)
        {
            //var node = new GenericNode(internode);
            
            // var newLower = new GenericInternode(cellCreator.CreateCell(10), node);

            //node.UpperInternode = newLower;
            
            //return node;

            throw new NotImplementedException();
        }
    }
}