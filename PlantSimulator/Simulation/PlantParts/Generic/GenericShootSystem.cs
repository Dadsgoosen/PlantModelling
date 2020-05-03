namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericShootSystem : IShootSystem
    {
        private readonly GenericStem genericStem;

        public IStem Stem => genericStem;

        public GenericShootSystem(GenericStem stem)
        {
            genericStem = stem;
        }
    }
}