using System.Numerics;
using PlantSimulator.Simulation.PlantParts.Generic;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public interface IStemPartFactory
    {
        public Stem CreateStem(Vector3 center, Node nodeConnection);
    }
}