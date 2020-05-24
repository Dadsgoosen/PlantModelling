using System;
using System.Numerics;
using System.Text.Json;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellSizer : ICellSizer
    {
        private readonly ICellCollisionDetection collisionDetection;

        public GenericCellSizer(ICellCollisionDetection collisionDetection)
        {
            this.collisionDetection = collisionDetection;
        }

        public void Resize(IPlantCell a, IPlantCell b)
        {
            float squishiness = CalculateSquishiness(a);

            Vector2[] aFace = a.Geometry.Face.Points;
            Vector2[] bFace = b.Geometry.Face.Points;

            foreach (var aPoint in aFace)
            {
                if (collisionDetection.IsPointInPolygon(aPoint, bFace))
                {
                    HandlePolygonCollision(aPoint, a.Geometry, b.Geometry, squishiness);
                }
            }
        }

        private void HandlePolygonCollision(Vector2 point, ICellGeometry face, ICellGeometry collidingFace, float squishiness)
        {
            Vector2 pointOnLine = collisionDetection.GetClosestPoint(point, collidingFace.Face.Points);

            Vector2 direction = (new Vector2(point.X, point.Y) - new Vector2(pointOnLine.X, pointOnLine.Y)) * squishiness;
        }

        private void MoveCenterPoint(Vector3 center, Vector2 direction)
        {
            MovePointFromDirectionVector(center, direction);
        }

        private void MoveFacePoints(Vector3[] face, Vector2 direction)
        {
            for (int i = 0; i < face.Length; i++)
            {
                MovePointFromDirectionVector(face[i], direction);
            }
        }

        private static void MovePointFromDirectionVector(Vector3 point, Vector2 direction)
        {
            point.X += direction.X;
            point.Z += direction.Y;
        }

        private static float CalculateSquishiness(IPlantCell cell)
        {
            return 1;
        }
    }
}