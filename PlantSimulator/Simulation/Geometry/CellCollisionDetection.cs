using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public class CellCollisionDetection : ICollisionDetection<IPlantCell>
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

        public IList<IPlantCell> GetNeighbors(IPlantCell a, IPlantCell[] entities)
        {
            var neighbors = new List<IPlantCell>();

            for (int i = 0; i < entities.Length; i++)
            {
                if (IsNeighbors(a, entities[i]))
                {
                    neighbors.Add(entities[i]);
                }
            }

            return neighbors;
        }

        public bool IsNeighbors(IPlantCell a, IPlantCell b)
        {
            if (!IsWithinYOfCell(a, b)) return false;

            IVertex[,] pairs = GetFacePairs(b.Geometry.Face.Points);

            for (int i = 0; i < pairs.GetLength(0); i++)
            {
                IVertex one = pairs[i, 0];
                IVertex two = pairs[i, 1];

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
        private bool IsPointInPolygon(IVertex p, IVertex[] polygon)
        {
            // Get the max X and Z
            float minX = polygon[0].X;
            float maxX = polygon[0].X;
            float minZ = polygon[0].Z;
            float maxZ = polygon[0].Z;

            for (int i = 1; i < polygon.Length; i++)
            {
                IVertex q = polygon[i];
                minX = Math.Min(q.X, minX);
                maxX = Math.Max(q.X, maxX);
                minZ = Math.Min(q.Z, minZ);
                maxZ = Math.Max(q.Z, maxZ);
            }
            
            bool inside = false;

            for (int i = 0, j = polygon.Length - 1; i < polygon.Length; j = i++)
            {
                if ((polygon[i].Z > p.Z) != (polygon[j].Z > p.Z) &&
                    p.X < (polygon[j].X - polygon[i].X) * (p.Z - polygon[i].Z) / (polygon[j].Z - polygon[i].Z) + polygon[i].X)
                {
                    inside = !inside;
                }
            }

            return inside;
        }

        private IVertex[,] GetFacePairs(IVertex[] polygon)
        {
            IVertex[,] pairs = new IVertex[polygon.Length, 2];

            int length = polygon.Length;

            for (int i = 0; i < length; i++)
            {
                if (i != length - 1)
                {
                    pairs[i, 0] = polygon[i];
                    pairs[i, 1] = polygon[i + 1];
                }
                else
                {
                    pairs[i, 0] = polygon[i];
                    pairs[i, 1] = polygon[0];
                }
            }
            return pairs;
        }
    }
}