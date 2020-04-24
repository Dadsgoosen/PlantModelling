using System.Threading.Tasks;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Runner
{
    public class GenericPlantRunner : IPlantRunner
    {
        public IPlant Plant { get; }

        public GenericPlantRunner(IPlant plant)
        {
            Plant = plant;
        }

        public void Tick()
        {
        }
    }
}