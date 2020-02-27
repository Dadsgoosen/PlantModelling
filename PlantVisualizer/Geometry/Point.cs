namespace PlantVisualizer.Geometry
{
    /// <summary>
    /// Object containing the coordinates for a point in a two-dimensional space
    /// </summary>
    public struct Point
    {
        /// <summary>
        /// Get the X coordinate
        /// </summary>
        public double X { get; }

        /// <summary>
        /// Get the Y coordinate
        /// </summary>
        public double Y { get; }

        /// <summary>
        /// Initialize a new Point in a two-dimensional space
        /// </summary>
        /// <param name="x">The <see cref="X"/> coordinate</param>
        /// <param name="y">The <see cref="Y"/> coordinate</param>
        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

    }
}

