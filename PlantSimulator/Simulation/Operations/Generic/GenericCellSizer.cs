using System.Collections.Generic;
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

            var bGeo = bCell.Geometry;

            var a = new Vector2(aGeo.TopCenter.X, aGeo.TopCenter.Z);

            var b = new Vector2(bGeo.TopCenter.X, bGeo.TopCenter.Z);

            var aPairs = geometryHelper.CreateFacePairs(aGeo.Face.Points);

            var bPairs = geometryHelper.CreateFacePairs(bGeo.Face.Points);

            Vector2[] aIntersecting = null;

            Vector2[] bIntersecting = null;

            for (int i = 0; i < aPairs.Length; i++)
            {
                if (aIntersecting != null && bIntersecting != null) break;

                if (geometryHelper.LinesIntersect(aPairs[i][0], aPairs[i][1], b, a))
                {
                    aIntersecting = aPairs[i];
                }

                if (geometryHelper.LinesIntersect(bPairs[i][0], bPairs[i][1], b, a))
                {
                    bIntersecting = bPairs[i];
                }
            }

            if (aIntersecting == null || bIntersecting == null)
            {
                logger.LogWarning("Faces are colliding but could not find two line pairs that collide with ab vector.");
                return;
            }

            Vector2 aIntersectingPoint = geometryHelper.IntersectingPoint(a, b, aIntersecting[0], aIntersecting[1]);

            Vector2 bIntersectingPoint = geometryHelper.IntersectingPoint(a, b, bIntersecting[0], bIntersecting[1]);

            float turgidity = aCell.Turgidity;

            Vector2 direction = (bIntersectingPoint - aIntersectingPoint) * turgidity;

            for (int i = 0; i < aGeo.Face.Points.Length; i++)
            {
                aGeo.Face.Points[i] += direction;
            }

            aGeo.TopCenter = new Vector3(aGeo.TopCenter.X + direction.X, aGeo.TopCenter.Y, aGeo.TopCenter.Z + direction.Y);

            aGeo.BottomCenter = new Vector3(bGeo.BottomCenter.X + direction.X, bGeo.BottomCenter.Y, bGeo.BottomCenter.Z + direction.Y);
        }
    }
}