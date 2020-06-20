using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public interface IInternodePartFactory
    {
        public Internode CreateInternode(Vector3 center, Node node, float height, int branchCount);
        public Internode CreateInternode(Vector3 center, float height, int branchCount);
    }
}