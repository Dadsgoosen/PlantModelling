namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class PlantPartDescriptor : IPlantPartDescriptor
    {
        public float Height { get; }

        public float Width { get; }

        public PlantPartDescriptor(float height, float width)
        {
            Height = height;
            Width = width;
        }
    }
}