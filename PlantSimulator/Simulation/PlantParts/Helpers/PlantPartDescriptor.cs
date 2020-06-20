using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class PlantPartDescriptor : IPlantPartDescriptor
    {
        public Vector3 Top { get; set; }

        public Vector3 Bottom { get; set; }

        public float Height { get; set; }

        public float WidthX { get; set; }

        public float WidthZ { get; set; }

        public float MaxX { get; set; }

        public float MaxY { get; set; }

        public float MaxZ { get; set; }

        public float MinX { get; set; }

        public float MinY { get; set; }

        public float MinZ { get; set; }

        public PlantPartDescriptor() { }
    }
}