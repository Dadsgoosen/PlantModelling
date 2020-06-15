using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Development
{
    public class PlantPartDeveloper : IPlantPartDeveloper
    {
        private readonly IPlantPartDevelopment<Internode> internodeDevelopment;

        public PlantPartDeveloper(IPlantPartDevelopment<Internode> internodeDevelopment)
        {
            this.internodeDevelopment = internodeDevelopment;
        }

        public void Develop(IPlantPart plantPart, SimulationStateSnapshot snapshot)
        {
            switch (plantPart)
            {
                case Internode internode:
                    internodeDevelopment.Develop(internode, snapshot);
                    break;
            }
        }
    }
}