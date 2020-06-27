using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Options
{
    public class SimulationEnvironmentOptions : ISimulationEnvironmentOptions
    {
        public float Temperature { get; set; }

        public IVertex LightSource { get; set; }

        public uint WateringInterval { get; set; }

        public float WateringAmount { get; set; }
    }
}