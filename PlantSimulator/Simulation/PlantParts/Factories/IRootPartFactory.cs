using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public interface IRootPartFactory
    {
        public Root CreateRoot(Vector3 center, int branchCount);
    }
}