using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface IPlantPartDescriptor
    {
        public Vector3 Top { get; }

        public Vector3 Bottom { get; }

        public float Height { get; }

        public float Width { get; }
    }
}