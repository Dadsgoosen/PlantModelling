using System;
using System.Numerics;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    /// <summary>
    /// Basic plant part descriptor service that describes the highest, lowest, width and height of a plant.
    /// </summary>
    public class PlantDescriptorService : IPlantDescriptorService
    {
        /// <summary>
        /// Describe the physical properties of a plant part
        /// </summary>
        /// <param name="part">The plant part to describe</param>
        /// <returns>A PlantPartDescriptor object describing the provided plant part</returns>
        /// <remarks>Involves looping over all cells in the plant part</remarks>
        public IPlantPartDescriptor Describe(IPlantPart part)
        {
            // Sum coordinates for computing averages
            float sumX = 0;
            float sumY = 0;
            float sumZ = 0;

            // Max and min values of all the x, y and z coordinates
            float xMax = float.MinValue;
            float yMax = float.MinValue;
            float zMax = float.MinValue;
            float xMin = float.MaxValue;
            float yMin = float.MaxValue;
            float zMin = float.MaxValue;

            // Cell counter
            var cellCount = 0;

            // Compute the above values
            foreach (var cell in part.Cells)
            {
                var top = cell.Geometry.TopCenter;
                var bottom = cell.Geometry.BottomCenter;

                xMax = Math.Max(xMax, top.X);
                yMax = Math.Max(yMax, top.Y);
                zMax = Math.Max(zMax, top.Z);
                xMin = Math.Min(xMin, bottom.X);
                yMin = Math.Min(yMin, bottom.Y);
                zMin = Math.Min(zMin, bottom.Z);

                sumX += top.X;
                sumY += top.Y;
                sumZ += top.Z;

                cellCount++;
            }

            // The average is computed by the total X and Z divided by the cellCount
            var center = new Vector2(sumX / cellCount, sumZ / cellCount);

            // The highest and lowest point, so that Y is yMax and yMin with the center as X and Z.
            var highest = new Vector3(center.X, yMax, center.Y);
            var lowest = new Vector3(center.X, yMin, center.Y);

            // The plant height is the euclidean distance between the highest and lowest point
            var height = Vector3.Distance(highest, lowest);

            var widestX = new Vector2(xMax, center.Y);
            var minX = new Vector2(xMax, center.Y);
            var widestZ = new Vector2(center.X, zMax);
            var minZ = new Vector2(center.X, zMax);

            // Thickness is computed by the euclidean distance between the center and widest coordinate
            var thicknessX = Vector2.Distance(center, widestX);
            var thicknessZ = Vector2.Distance(center, widestZ);

            return new PlantPartDescriptor
            {
                Bottom = lowest,
                Height = height,
                MaxX = xMax,
                MaxY = yMax,
                MaxZ = zMax,
                MinX = xMin,
                MinY = yMin,
                MinZ = zMin,
                WidthX = thicknessX,
                WidthZ = thicknessZ
            };
        }
    }
}