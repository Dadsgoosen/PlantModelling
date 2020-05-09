using System.Numerics;

namespace PlantSimulatorService.Simulations.Model.Options
{
    public class SimulationEnvironmentOptions : ISimulationEnvironmentOptions
    {
        public float Temperature { get; set; }

        public Vector2 LightSource { get; set; }
    }
}