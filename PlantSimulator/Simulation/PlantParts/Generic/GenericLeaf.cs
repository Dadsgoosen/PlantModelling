namespace PlantSimulator.Simulation.PlantParts.Generic
{
    public class GenericLeaf : Leaf
    {
        public GenericLeaf(Petiole petiole)
        {
            Petiole = petiole;
            Connections = new[] {Petiole};
        }
    }
}