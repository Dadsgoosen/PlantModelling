using System;

namespace PlantSimulator.Helpers
{
    internal static class RangeExtensions
    {
        public static Random Random;

        public static int RandomNumberBetween(this Range<int> range)
        {
            return Random.Next(range.Min, range.Max);
        }

        public static float RandomNumberBetween(this Range<float> range)
        {
            return (float) Math.Floor(Random.NextDouble() * range.Max) + range.Min;
        }
    }
}