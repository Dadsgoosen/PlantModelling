using System;
using System.Collections.Specialized;
using System.Numerics;

namespace PlantSimulator.Simulation.Geometry
{
    public class GeometryHelper : IGeometryHelper
    {
        private const float Tolerance = 0.0001F;

        public bool IsOnLine(Vector2 point, Vector2[] line)
        {
            float a = Vector2.Distance(line[0], point);
            float b = Vector2.Distance(point, line[1]);
            float c = Vector2.Distance(line[0], line[1]);
            return a + b - c < 0.0001;
        }

        public Vector2 NearestPointOnLine(Vector2 p, Vector2[] line)
        {
            float apx = p.X - line[0].X;
            float apz = p.Y - line[0].Y;
            float abx = line[1].X - line[0].X;
            float abz = line[1].Y - line[0].Y;

            float ab2 = abx * abx + abz * abz;
            float apAb = apx * abx + apz * abz;

            float t = apAb / ab2;

            if (t < 0)
            {
                t = 0;
            }
            else if (t > 1)
            {
                t = 1;
            }

            float vx = line[0].X + abx * t;
            float vz = line[0].Y + abz * t;

            return new Vector2(vx, vz);
        }

        public bool IsInsidePolygon(Vector2 p, Vector2[] polygon)
        {
            // Get the max X and Z
            float minX = polygon[0].X;
            float maxX = polygon[0].X;
            float minZ = polygon[0].Y;
            float maxZ = polygon[0].Y;

            for (int i = 1; i < polygon.Length; i++)
            {
                Vector2 q = polygon[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minZ = Math.Min(q.Y, minZ);
                maxZ = Math.Max(q.Y, maxZ);
            }

            bool inside = false;

            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((polygon[i].Y > p.Y) != (polygon[j].Y > p.Y) &&
                    p.X < (polygon[j].X - polygon[i].X) * (p.Y - polygon[i].Y) / (polygon[j].Y - polygon[i].Y) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        public bool LinesIntersect(Vector2[] a, Vector2[] b)
        {
            return LinesIntersect(a[0], a[1], b[0], b[1]);
        }

        public bool LinesIntersect(Vector2 a, Vector2 b, Vector2 c, Vector2 d)
        {
            float denominator = (b.X - a.X) * (d.Y - c.Y) - (b.Y - a.Y) * (d.X - c.X);
            float numerator1 = (a.Y - c.Y) * (d.X - c.X) - (a.X - c.X) * (d.Y - c.Y);
            float numerator2 = (a.Y - c.Y) * (b.X - a.X) - (a.X - c.X) * (b.Y - a.Y);

            // Detect coincident lines
            if (Math.Abs(denominator) < Tolerance) return Math.Abs(numerator1) < Tolerance && Math.Abs(numerator2) < Tolerance;

            float r = numerator1 / denominator;
            float s = numerator2 / denominator;

            return (r >= 0 && r <= 1) && (s >= 0 && s <= 1);
        }

        public Vector2 IntersectingPoint(Vector2[] a, Vector2[] b)
        {
            return IntersectingPoint(a[0], a[1], b[0], b[1]);
        }

        public Vector2 IntersectingPoint(Vector2 p1, Vector2 p2, Vector2 p3, Vector2 p4)
        {
            // Get the segments' parameters.
            float dx12 = p2.X - p1.X;
            float dy12 = p2.Y - p1.Y;
            float dx34 = p4.X - p3.X;
            float dy34 = p4.Y - p3.Y;

            // Solve for t1 and t2
            float denominator = (dy12 * dx34 - dx12 * dy34);

            float t1 =
                ((p1.X - p3.X) * dy34 + (p3.Y - p1.Y) * dx34)
                / denominator;

            if (float.IsInfinity(t1))
            {
                throw new ArgumentException("The lines does not intersect and therefore does not exist a point where the lines intersect.");
            }

            float t2 =
                ((p3.X - p1.X) * dy12 + (p1.Y - p3.Y) * dx12)
                / -denominator;

            // Find the point of intersection.
            return new Vector2(p1.X + dx12 * t1, p1.Y + dy12 * t1);
        }

        public bool IsWithinHeight(Vector3 point, Vector3 top, Vector3 bottom, bool onLine)
        {
            if (onLine)
            {
                return point.Y <= top.Y && point.Y >= bottom.Y;
            }
            return point.Y < top.Y && point.Y > bottom.Y;
        }

        public Vector2[][] CreateFacePairs(Vector2[] polygon)
        {
            Vector2[][] pairs = new Vector2[polygon.Length][];

            int length = polygon.Length;

            for (int i = 0; i < length; i++)
            {
                if (i != length - 1)
                {
                    pairs[i] = new[] { polygon[i], polygon[i + 1] };
                }
                else
                {
                    pairs[i] = new[] { polygon[i], polygon[0] };
                }
            }
            return pairs;
        }
    }
}