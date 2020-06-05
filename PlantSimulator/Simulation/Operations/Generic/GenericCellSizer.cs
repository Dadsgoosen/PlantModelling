using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Numerics;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellSizer : ICellSizer
    {
        private readonly IGeometryHelper geometryHelper;

        private readonly ILoggerAdapter<GenericCellSizer> logger;

        public GenericCellSizer(IGeometryHelper geometryHelper, ILoggerAdapter<GenericCellSizer> logger)
        {
            this.geometryHelper = geometryHelper;
            this.logger = logger;
        }

        public void ResizeWidth(IPlantCell a, IPlantCell b)
        {
            ResizeFace(a, b);
        }

        public void ResizeHeight(IPlantCell a, IPlantCell b)
        {
            ResizeHeight(a.Geometry, b.Geometry);
        }

        private static void ResizeHeight(ICellGeometry a, ICellGeometry b)
        {
            // First we need to check whether either a top or a bottom is between b top or b bottom.
            bool between = a.TopCenter.Y < b.TopCenter.Y && a.TopCenter.Y > b.BottomCenter.Y ||
                           a.BottomCenter.Y > b.BottomCenter.Y && a.BottomCenter.Y < b.TopCenter.Y;

            // If it is not between, then we don't need to do anything
            if (!between) return;
            
            // If the top of cell a sticks through to bottom of cell b above
            if (a.TopCenter.Y > b.BottomCenter.Y)
            {
                float difference = a.TopCenter.Y - b.BottomCenter.Y;

                a.TopCenter = new Vector3(a.TopCenter.X, b.BottomCenter.Y, a.TopCenter.Z);

                a.BottomCenter = new Vector3(a.BottomCenter.X, a.BottomCenter.Y - difference, a.BottomCenter.Z);
            }

            // If the bottom of cell a sticks through the top of cell b below
            if (a.BottomCenter.Y < b.TopCenter.Y)
            {
                float difference = b.TopCenter.Y - a.BottomCenter.Y;

                a.BottomCenter = new Vector3(a.BottomCenter.X, b.TopCenter.Y, a.BottomCenter.Z);

                a.TopCenter = new Vector3(a.TopCenter.X, a.TopCenter.Y + difference, a.TopCenter.Z);
            }
        }

        private void ResizeFace(IPlantCell aCell, IPlantCell bCell)
        {
            var aGeo = aCell.Geometry;
            var aFace = aGeo.Face.Points;

            var bGeo = bCell.Geometry;
            var bFace = bGeo.Face.Points;

            var pairs = geometryHelper.CreateFacePairs(bFace);

            for (int i = 0; i < aFace.Length; i++)
            {
                if (!geometryHelper.IsInsidePolygon(aFace[i], bFace)) continue;

                Vector2 nearest = FindNearestPoint(aFace[i], pairs);

                Vector2 direction = (aFace[i] - nearest) * aCell.Turgidity;
                
                aFace[i] = nearest;

                ChangeFace(aGeo, aFace[i], direction);
            }
        }

        private Vector2 FindNearestPoint(Vector2 point, Vector2[][] pairs)
        {
            float distance = float.MaxValue;

            Vector2 nearest = Vector2.Zero;

            for (int i = 0; i < pairs.Length; i++)
            {
                Vector2 n = geometryHelper.NearestPointOnLine(point, pairs[i]);

                float distToNearestPoint = Vector2.Distance(point, n);

                if (distToNearestPoint < distance)
                {
                    distance = distToNearestPoint;

                    nearest = n;
                }
            }

            return nearest;
        }

        private static void ChangeFace(ICellGeometry geo, Vector2 changedPoint, Vector2 direction)
        {
            geo.TopCenter += new Vector3(direction.X, 0, direction.Y);
            
            geo.BottomCenter += new Vector3(direction.X, 0, direction.Y);

            for (int i = 0; i < geo.Face.Points.Length; i++)
            {
                if (geo.Face.Points[i] == changedPoint) continue;
                geo.Face.Points[i] += direction;
            }
        }
    }
}