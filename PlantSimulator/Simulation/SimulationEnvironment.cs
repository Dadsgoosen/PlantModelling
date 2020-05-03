using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation
{
    public class SimulationEnvironment
    {
        public IVertex LightPosition { get; set; } = new Vertex(0, 10000, 0);

        public float Temperature { get; set; } = 22;
    }
}