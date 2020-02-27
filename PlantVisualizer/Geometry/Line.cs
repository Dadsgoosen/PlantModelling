namespace PlantVisualizer.Geometry
{
    public struct Line
    {
        public Point Start { get; }

        public Point End { get; }

        public Line(Point start, Point end)
        {
            Start = start;
            End = end;
        }
    }
}