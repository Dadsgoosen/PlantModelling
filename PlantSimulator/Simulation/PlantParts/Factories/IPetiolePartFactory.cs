using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public interface IPetiolePartFactory
    {
        public Petiole CreatePetiole(Vector3 center, Vector3 direction);
    }
}