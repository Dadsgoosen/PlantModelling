namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericShootSystem : IShootSystem
    {
        private readonly GenericStem genericStem;

        public Stem Stem => genericStem;

        public GenericShootSystem(GenericStem stem)
        {
            genericStem = stem;
        }
    }
}