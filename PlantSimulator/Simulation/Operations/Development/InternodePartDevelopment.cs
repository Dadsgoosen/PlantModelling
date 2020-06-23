using System;
using System.Threading.Tasks;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells.Helpers;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.Operations.Development
{
    public class InternodePartDevelopment : IPlantPartDevelopment<Internode>
    {
        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly INodePartFactory nodePartFactory;

        private readonly ICellGrower cellGrower;

        private readonly IInternodePartFactory internodeFactory;

        private readonly IPlantDescriptorService plantDescriptorService;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public InternodePartDevelopment(IPlantSimulatorOptionsService optionsService,
            INodePartFactory nodePartFactory,
            ICellGrower cellGrower,
            IPlantDescriptorService plantDescriptorService, 
            IInternodePartFactory internodeFactory)
        {
            this.optionsService = optionsService;
            this.nodePartFactory = nodePartFactory;
            this.cellGrower = cellGrower;
            this.plantDescriptorService = plantDescriptorService;
            this.internodeFactory = internodeFactory;
        }

        public void Develop(Internode internode, SimulationStateSnapshot snapshot)
        {
            var description = plantDescriptorService.Describe(internode, true);

            if (ShouldGrow(internode, description))
            {
                GrowInternode(internode, snapshot);
            }

            if (ShouldAddNewNode(internode, snapshot, description.Height))
            {
                internode.UpperNode = CreateNewUpperNode(internode, description);
            }
        }

        private bool ShouldGrow(Internode internode, IPlantPartDescriptor descriptor)
        {
            var terminalHeight = Options.Plant.TerminalHeight.RandomNumberBetween();

            var isBelowTerminalHeight = descriptor.Height <= terminalHeight;

            return !internode.HasUpperNode() && isBelowTerminalHeight;
        }

        private void GrowInternode(Internode internode, SimulationStateSnapshot snapshot)
        {
            CellIterator.IterateCells(internode.Cells, cell => cellGrower.GrowShootCell(cell, internode, snapshot));
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

            uint due = (uint) Options.Plant.NewNodeTickCount.RandomNumberBetween();

            if (snapshot.CurrentTime >= due && snapshot.CurrentTime % due == 0)
            {
                return true;
            }

            return false;
        }

        private Node CreateNewUpperNode(Internode internode, IPlantPartDescriptor descriptor)
        {
            var node = nodePartFactory.CreateNode(internode, descriptor, true);

            var upperInternode = internodeFactory.CreateInternode(descriptor.Top, node, 0, internode.BranchCount);

            node.UpperInternode = upperInternode;

            return node;
        }
    }
}