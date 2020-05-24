
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public interface ICellCollisionDetection
    {
        public bool IsColliding(IPlantCell a, IPlantCell b);
        public bool IsHeightColliding(IPlantCell a, IPlantCell b);
        public IList<IPlantCell> GetNeighbors(IPlantCell a, IPlantCell[] entities);
        public bool AreNeighbors(IPlantCell a, IPlantCell b);
        public Vector2 GetClosestPoint(Vector2 point, Vector2[] polygon);
        public bool IsPointInPolygon(Vector2 p, Vector2[] polygon);
    }
}