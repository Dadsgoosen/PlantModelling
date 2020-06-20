using System.Collections.Generic;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    public interface IPlantPartFactory
    {
        public Stem CreateStem(Internode internode, int branchCount);
        public Internode CreateInternode(float radius);
        public Internode CreateInternode(float radius, Node lowerNode);
        public Node CreateNode(Internode lowerInternode, IEnumerable<Stem> stems, IEnumerable<Petiole> petioles);
        public Root CreateRoot(float radius);
        public Root CreateRoot(float radius, Root connection);
        public Petiole CreatePetiole(Node node);
    }
}