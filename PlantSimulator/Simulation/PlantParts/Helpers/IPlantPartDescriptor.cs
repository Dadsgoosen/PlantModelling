using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface IPlantPartDescriptor
    {
        /// <summary>
        /// The top center point
        /// </summary>
        public Vector3 Top { get; set; }

        /// <summary>
        /// The bottom center point
        /// </summary>
        public Vector3 Bottom { get; set; }

        /// <summary>
        /// The height between <see cref="Top"/> and <see cref="Bottom"/>
        /// </summary>
        public float Height { get; set; }

        /// <summary>
        /// The width between center and <see cref="MaxX"/>
        /// </summary>
        public float WidthX { get; set; }

        /// <summary>
        /// The width between center and <see cref="MaxZ"/>
        /// </summary>
        public float WidthZ { get; set; }

        /// <summary>
        /// The highest X value
        /// </summary>
        public float MaxX { get; set; }

        /// <summary>
        /// The highest Y value
        /// </summary>
        public float MaxY { get; set; }

        /// <summary>
        /// The highest Z value
        /// </summary>
        public float MaxZ { get; set; }

        /// <summary>
        /// The lowest x value
        /// </summary>
        public float MinX { get; set; }

        /// <summary>
        /// The lowest Y value
        /// </summary>
        public float MinY { get; set; }

        /// <summary>
        /// The lowest z value
        /// </summary>
        public float MinZ { get; set; }
    }
}