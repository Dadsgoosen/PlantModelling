using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public class CellCollisionDetection : ICellCollisionDetection
    {
        private const float Tolerance = 0.001F;

        public bool IsColliding(IPlantCell a, IPlantCell b)
        {
            if (!IsWithinYOfCell(a, b)) return false;

            for (int i = 0; i < a.Geometry.Face.Points.Length; i++)
            {
                if (IsPointInPolygon(a.Geometry.Face.Points[i], b.Geometry.Face.Points))
                {
                    return true;
                }
            }

            return false;
        }

        public bool IsHeightColliding(IPlantCell a, IPlantCell b)
        {
            return IsWithinYOfCell(a, b);
        }

        public IList<IPlantCell> GetNeighbors(IPlantCell a, IPlantCell[] entities)
        {
            var neighbors = new List<IPlantCell>();

            for (int i = 0; i < entities.Length; i++)
            {
                if (AreNeighbors(a, entities[i]))
                {
                    neighbors.Add(entities[i]);
                }
            }

            return neighbors;
        }

        public bool AreNeighbors(IPlantCell a, IPlantCell b)
        {
            return IsColliding(a, b);
            // Neighbor check has been reduced to check if they colliding,
            // since this also gives the true if they are on the line as well.

            /*
            if (!IsWithinYOfCell(a, b)) return false;

            IVertex[][] pairs = GetFacePairs(b.Geometry.Face.Points);

            

            for (int i = 0; i < pairs.GetLength(0); i++)
            {
                IVertex one = pairs[i][0];
                IVertex two = pairs[i][1];

                foreach (var point in a.Geometry.Face.Points)
                {
                    float dxc = point.X - one.X;
                    float dyc = point.Z - one.Z;

                    float dxl = two.X - one.X;
                    float dyl = two.Z - one.Z;

                    float cross = dxc * dyl - dyc * dxl;

                    if (Math.Abs(cross) < Tolerance)
                    {
                        return true;
                    }
                }
            }

            return false;
            */
        }

        public Vector2 GetClosestPoint(Vector2 p, Vector2[] polygon)
        {
            Vector2 point = new Vector2(p.X, p.Y);

            float lowestDistance = float.MaxValue;

            Vector2 closest = new Vector2(p.X, p.Y);

            Vector2[][] pairs = GetFacePairs(polygon);

            for (int i = 0; i < pairs.GetLength(0); i++)
            {
                Vector2 nearestPoint = NearestPointOnLine(p, pairs[i]);

                float distance = Vector2.Distance(point, nearestPoint);

                if (distance < lowestDistance) 
                {
                    lowestDistance = distance;

                    closest = nearestPoint;
                }
            }

            return new Vector2(closest.X, closest.Y);
        }

        private Vector2 NearestPointOnLine(Vector2 p, Vector2[] line)
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

        /// <summary>
        /// Checks whether a plant cells height is within the height of another cell.
        /// </summary>
        /// <param name="a">The plant to check if is within the height of <see cref="b"/></param>
        /// <param name="b">The plant that is checked against</param>
        /// <returns>True if the height of <see cref="a"/> is within the height <see cref="b"/></returns>
        private static bool IsWithinYOfCell(IPlantCell a, IPlantCell b)
        {
            return a.Geometry.TopCenter.Y <= b.Geometry.TopCenter.Y || a.Geometry.BottomCenter.Y >= b.Geometry.BottomCenter.Y;
        }

        /// <summary>
        /// Tests whether a point is inside a polygon
        /// </summary>
        /// <param name="p">The point to test if it inside the polygon</param>
        /// <param name="polygon">The polygon to test the point <see cref="p"/> against</param>
        /// <remarks>http://www.ecse.rpi.edu/Homepages/wrf/Research/Short_Notes/pnpoly.html</remarks>
        /// <returns>True if point <see cref="p"/> is inside <see cref="polygon"/>, false if not</returns>
        public bool IsPointInPolygon(Vector2 p, Vector2[] polygon)
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

        private Vector2[][] GetFacePairs(Vector2[] polygon)
        {
            Vector2[][] pairs = new Vector2[polygon.Length][];

            int length = polygon.Length;

            for (int i = 0; i < length; i++)
            {
                if (i != length - 1)
                {
                    pairs[i] = new [] {polygon[i], polygon[i + 1]};
                }
                else
                {
                    pairs[i] = new [] {polygon[i], polygon[0]};
                }
            }
            return pairs;
        }
    }
}