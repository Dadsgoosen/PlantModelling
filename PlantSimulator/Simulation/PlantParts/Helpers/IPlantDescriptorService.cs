namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    /// <summary>
    /// Interface for all plant part descriptors
    /// </summary>
    public interface IPlantDescriptorService
    {
        /// <summary>
        /// Describe the physical properties of a plant part
        /// </summary>
        /// <param name="part">The plant part to describe</param>
        /// <returns>A PlantPartDescriptor object describing the provided plant part</returns>
        public IPlantPartDescriptor Describe(IPlantPart part);
    }
}