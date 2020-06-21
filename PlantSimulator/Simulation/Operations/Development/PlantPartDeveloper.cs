using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Development
{
    public class PlantPartDeveloper : IPlantPartDeveloper
    {
        private readonly IPlantPartDevelopment<Internode> internodeDevelopment;

        private readonly IPlantPartDevelopment<Root> rootDevelopment;

        public PlantPartDeveloper(IPlantPartDevelopment<Internode> internodeDevelopment, IPlantPartDevelopment<Root> rootDevelopment)
        {
            this.internodeDevelopment = internodeDevelopment;
            this.rootDevelopment = rootDevelopment;
        }

        public void Develop(IPlantPart plantPart, SimulationStateSnapshot snapshot)
        {
            switch (plantPart)
            {
                case Internode internode:
                    internodeDevelopment.Develop(internode, snapshot);
                    break;
                case Root root:
                    rootDevelopment.Develop(root, snapshot);
                    break;
            }
        }
    }
}