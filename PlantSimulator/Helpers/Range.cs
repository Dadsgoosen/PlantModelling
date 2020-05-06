using System;
using Google.Protobuf.Collections;

namespace PlantSimulator.Helpers
{
    public readonly struct Range<T>
    {
        public T Max { get; }

        public T Min { get; }

        public Range(T min, T max)
        {
            Max = max;
            Min = min;
        }

        public static Range<TK> Create<TK>(TK min, TK max)
        {
            return new Range<TK>(min, max);
        }

        public static implicit operator Range<T>(T[] range)
        {
            return range.Length switch
            {
                1 => Create(range[0], range[0]),
                2 => Create(range[0], range[1]),
                _ => throw new ArgumentException("Implicit conversion from array to range must be either length 1 or 2",
                    nameof(range))
            };
        }

        public static implicit operator Range<T>(RepeatedField<T> range)
        {
            return range.Count switch
            {
                1 => Create(range[0], range[0]),
                2 => Create(range[0], range[1]),
                _ => throw new ArgumentException("Implicit conversion from array to range must be either length 1 or 2",
                    nameof(range))
            };
        }
    }
}