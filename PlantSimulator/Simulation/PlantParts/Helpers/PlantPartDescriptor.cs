using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class PlantPartDescriptor : IPlantPartDescriptor
    {
        public Vector3 Top { get; set; }

        public Vector3 Bottom { get; set; }

        public float Height { get; set; }

        public float Width { get; set; }

        public PlantPartDescriptor(Vector3 top, Vector3 bottom, float height, float width)
        {
            Top = top;
            Bottom = bottom;
            Height = height;
            Width = width;
        }
    }
}