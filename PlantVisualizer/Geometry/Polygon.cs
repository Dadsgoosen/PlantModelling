using System.Collections.Generic;

namespace PlantVisualizer.Geometry
{
    public class Polygon
    {
        public IList<Point> Points { get; }

        public Polygon(IEnumerable<Point> points)
        {
            Points = new List<Point>(points);
        }
       
    }
}