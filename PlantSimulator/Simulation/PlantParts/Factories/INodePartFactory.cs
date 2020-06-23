using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public interface INodePartFactory
    {
        public GenericNode CreateNode(Internode lowerInternode, IPlantPartDescriptor lowerInternodeDescriptor, bool withOptions);
    }
}