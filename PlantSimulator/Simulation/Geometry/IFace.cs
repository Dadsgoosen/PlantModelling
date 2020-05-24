using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;

namespace PlantSimulator.Simulation.Geometry
{
    public interface IFace
    {
        public Vector2[] Points { get; }

        public float Area
        {
            get
            {
                // Add the first point to the end.
                int numPoints = Points.Length;

                Vector2[] pts = new Vector2[numPoints + 1];
                
                Points.CopyTo(pts, 0);
                
                pts[numPoints] = Points[0];

                // Compute the area
                float area = 0;

                for (int i = 0; i < numPoints; i++)
                {
                    area +=
                        (pts[i + 1].X - pts[i].X) *
                        (pts[i + 1].Y + pts[i].Y) / 2;
                }

                // Return the result.
                return Math.Abs(area);
            }
        }

        public bool Equals(IFace face);
    }
}