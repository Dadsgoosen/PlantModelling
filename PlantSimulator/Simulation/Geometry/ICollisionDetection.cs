
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public interface ICellCollisionDetection
    {
        public bool Colliding(IPlantCell a, IPlantCell b, bool allowOnLine);
        public bool Neighbors(IPlantCell a, IPlantCell b, bool allowInside);
    }
}