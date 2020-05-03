using System.Threading.Tasks;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Runner
{
    public class GenericPlantRunner : IPlantRunner
    {
        public IPlant Plant { get; }

        private readonly IPlantGrower plantGrower;

        public GenericPlantRunner(IPlant plant, IPlantGrower plantGrower)
        {
            Plant = plant;
            this.plantGrower = plantGrower;
        }

        public void Tick(SimulationStateSnapshot state)
        {
            plantGrower.GrowPlant(Plant, state);
        }
    }
}