
using PlantSimulator.Outputs.Models;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Outputs
{
    public class SimulationStateFactory : ISimulationStateFactory
    {
        public SimulationState Create(IPlant plant, SimulationStateData data)
        {
            return new SimulationState
            {
                Id = data.Id,
                SimulationTime = data.SimulationTimer,
                Plant = CreatePlantState(plant)
            };
        }

        private PlantModelState CreatePlantState(IPlant plant)
        {
            return null;
        }
    }
}