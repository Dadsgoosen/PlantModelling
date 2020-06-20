using System;
using PlantSimulator.Helpers;
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

        private readonly IPlantDescriptorService plantDescriptorService;

        public InternodePartDevelopment(IPlantSimulatorOptionsService optionsService,
            INodePartFactory nodePartFactory,
            ICellGrower cellGrower,
            IPlantDescriptorService plantDescriptorService)
        {
            this.optionsService = optionsService;
            this.nodePartFactory = nodePartFactory;
            this.cellGrower = cellGrower;
            this.plantDescriptorService = plantDescriptorService;
        }

        private IPlantSimulatorOptions Options => optionsService.Options;

        public void Develop(Internode internode, SimulationStateSnapshot snapshot)
        {
            var description = plantDescriptorService.Describe(internode);

            if (ShouldGrow(internode))
            {
                GrowInternode(internode, snapshot);
            }

            if (ShouldAddNewNode(internode, snapshot, description.Height))
            {
                internode.UpperNode = CreateNewUpperNode(internode, description);
            }
        }

        private bool ShouldGrow(Internode internode)
        {
            return !internode.HasUpperNode();
        }

        private void GrowInternode(Internode internode, SimulationStateSnapshot snapshot)
        {
            foreach (var cell in internode.Cells)
            {
                cellGrower.GrowShootCell(cell, internode, snapshot);
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

            uint due = (uint) Options.Plant.NewNodeTickCount.RandomNumberBetween();

            if (snapshot.CurrentTime >= due)
            {
                return true;
            }

            return false;
        }

        private Node CreateNewUpperNode(Internode internode, IPlantPartDescriptor descriptor)
        {
            return nodePartFactory.CreateNode(internode, descriptor, true);
        }
    }
}