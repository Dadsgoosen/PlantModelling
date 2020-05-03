using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericPlantRunner : IPlantRunner
    {
        private readonly IPlantGrower plantGrower;

        public IPlant Plant { get; }

        public SimulationEnvironment Environment { get; }

        public GenericPlantRunner(IPlant plant, SimulationEnvironment environment, IPlantGrower plantGrower)
        {
            this.plantGrower = plantGrower;
            Plant = plant;
            Environment = environment;
        }

        public void Tick(SimulationStateSnapshot stateSnapshot)
        {
            plantGrower.GrowPlant(Plant, stateSnapshot);
        }
    }
}