using System.Numerics;

namespace PlantSimulator.Simulation.Geometry
{
    public interface IGeometryHelper
    {
        public bool IsOnLine(Vector2 p, Vector2[] line);
        
        public Vector2 NearestPointOnLine(Vector2 p, Vector2[] line);
        
        public Vector2[][] CreateFacePairs(Vector2[] polygon);

        /// <summary>
        /// Tests whether a point is inside a polygon
        /// </summary>
        /// <param name="p">The point to test if it inside the polygon</param>
        /// <param name="polygon">The polygon to test the point <see cref="p"/> against</param>
        /// <remarks>http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html</remarks>
        /// <returns>True if point <see cref="p"/> is inside <see cref="polygon"/>, false if not</returns>
        public bool IsInsidePolygon(Vector2 p, Vector2[] polygon);

        public bool LinesIntersect(Vector2[] a, Vector2[] b);
        public bool LinesIntersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d);
        public Vector2 IntersectingPoint(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4);
        public Vector2 IntersectingPoint(Vector2[] a, Vector2[] b);

        public bool IsWithinHeight(Vector3 point, Vector3 top, Vector3 bottom, bool onLine);
    }
}