namespace PlantSimulator.Outputs.Models
{
    /// <summary>
    /// Data Access Object for a 2D point to describe the plant.
    /// </summary>
    public class PlantNodeModelState
    {
        /// <summary>
        /// X coordinate for the plant part
        /// </summary>
        public int X { get; set; }

        /// <summary>
        /// Y coordinate for the plant part
        /// </summary>
        public int Y { get; set; }

        /// <summary>
        /// Thickness of the plant at the (<see cref="X"/>, <see cref="Y"/>)
        /// </summary>
        public int Thickness { get; set; }
    }
}