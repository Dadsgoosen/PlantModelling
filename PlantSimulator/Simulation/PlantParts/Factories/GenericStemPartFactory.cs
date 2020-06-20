using System;
using System.Numerics;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public class GenericStemPartFactory : IStemPartFactory
    {
        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly IInternodePartFactory internodePartFactory;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public GenericStemPartFactory(IPlantSimulatorOptionsService optionsService, IInternodePartFactory internodePartFactory)
        {
            this.optionsService = optionsService;
            this.internodePartFactory = internodePartFactory;
        }

        public Stem CreateStem(Vector3 center, Node nodeConnection)
        {
            // Since the node connection is the branching node, this stem will be a new branch and therefore + 1
            int stemBranchCount = nodeConnection.BranchCount + 1;

            Internode internode = CreateInternode(center, stemBranchCount);

            return new GenericStem(internode, stemBranchCount);
        }

        public Internode CreateInternode(Vector3 center, int branchCount)
        {
            return internodePartFactory.CreateInternode(center, 0, branchCount);
        }
    }
}