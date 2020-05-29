using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellSizer : ICellSizer
    {
        private readonly IGeometryHelper geometryHelper;

        public GenericCellSizer(IGeometryHelper geometryHelper)
        {
            this.geometryHelper = geometryHelper;
        }

        public void Resize(IPlantCell a, IPlantCell b)
        {
            ResizeHeight(a.Geometry, b.Geometry);
            ResizeFace(a.Geometry, b.Geometry);
        }

        private static void ResizeHeight(ICellGeometry a, ICellGeometry b)
        {
            if (a.TopCenter.Y < b.TopCenter.Y)
            {
                a.TopCenter = new Vector3(a.TopCenter.X, b.TopCenter.Y, a.TopCenter.Z);
            }

            if (a.BottomCenter.Y > b.BottomCenter.Y)
            {
                a.BottomCenter = new Vector3(a.BottomCenter.X, b.BottomCenter.Y, a.BottomCenter.Z);
            }
        }

        private void ResizeFace(ICellGeometry a, ICellGeometry b)
        {
            Vector2 aCenter = new Vector2(a.TopCenter.X, a.TopCenter.Z);
            Vector2 bCenter = new Vector2(b.TopCenter.X, b.TopCenter.Z);

            Vector2 direction = Vector2.Normalize(bCenter - aCenter);
        }

        private Vector2 ComputeDirectionVector(Vector2[] face, Vector2[] bFace)
        {
            var collidingPoints = GetCollidingPoints(face, bFace);

            var mean = Vector2.Zero;

            foreach (var point in collidingPoints)
            {
                mean += point;
            }

            mean /= collidingPoints.Count;

            return Vector2.Zero;
        }

        private IList<Vector2> GetCollidingPoints(Vector2[] face, Vector2[] collidedFace)
        {
            IList<Vector2> colliding = new List<Vector2>(face.Length);

            foreach (var point in face)
            {
                if (!geometryHelper.IsInsidePolygon(point, collidedFace)) continue;

                colliding.Add(point);
            }

            return colliding;
        }
    }
}