using System;

namespace PlantSimulator.Helpers
{
    internal static class RangeExtensions
    {
        public static Random Random;

        /// <summary>
        /// Get a random integer between the provided range
        /// </summary>
        /// <param name="range">The minimum and maximum integer range</param>
        /// <returns>A random integer between minimum and maximum</returns>
        public static int RandomNumberBetween(this Range<int> range)
        {
            return Random.Next(range.Min, range.Max);
        }

        /// <summary>
        /// Get a random float between the provided range
        /// </summary>
        /// <param name="range">The minimum and maximum float range</param>
        /// <returns>A random float between minimum and maximum</returns>
        public static float RandomNumberBetween(this Range<float> range)
        {
            return (float) Math.Floor(Random.NextDouble() * range.Max) + range.Min;
        }
    }
}