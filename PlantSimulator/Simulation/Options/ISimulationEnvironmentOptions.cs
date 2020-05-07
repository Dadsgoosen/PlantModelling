using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Options
{
    public interface ISimulationEnvironmentOptions
    {
        public float Temperature { get; set; }

        public IVertex LightSource { get; set; }
    }
}