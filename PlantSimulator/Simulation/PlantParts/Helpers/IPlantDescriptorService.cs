namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface IPlantDescriptorService
    {
        public IPlantPartDescriptor Describe(IPlantPart part);
    }
}