
using System.Collections.Generic;

namespace PlantSimulator.Simulation.Geometry
{
    public interface ICollisionDetection<T>
    {
        public bool IsColliding(T a, T b);
        public IList<T> GetNeighbors(T a, T[] entities);
        public bool IsNeighbors(T a, T b);
    }
}