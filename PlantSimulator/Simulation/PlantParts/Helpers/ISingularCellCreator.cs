using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public interface ISingularCellCreator
    {
        public IPlantCell CreateCell(PlantCellType type, Vector3 center, float radius, float height);
    }
}