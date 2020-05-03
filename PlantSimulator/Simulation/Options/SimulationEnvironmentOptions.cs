using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Options
{
    public class SimulationEnvironmentOptions
    {
        public float Temperature { get; set; }

        public IVertex LightSource { get; set; }
    }
}