using System;
using System.Collections.Generic;
using System.Drawing;

namespace PlantSimulator.Simulation.Geometry
{
    public interface IFace
    {
        public IVertex[] Points { get; }

        public float Area
        {
            get
            {
                // Add the first point to the end.
                int num_points = Points.Length;
                IVertex[] pts = new IVertex[num_points + 1];
                Points.CopyTo(pts, 0);
                pts[num_points] = Points[0];

                // Get the areas.
                float area = 0;
                for (int i = 0; i < num_points; i++)
                {
                    area +=
                        (pts[i + 1].X - pts[i].X) *
                        (pts[i + 1].Z + pts[i].Z) / 2;
                }

                // Return the result.
                return Math.Abs(area);
            }
        }

        public bool Equals(IFace face);
    }
}