using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Factories
{
    /*public class GenericPlantPartFactory : IPlantPartFactory
    {
        private readonly GenericStemPartFactory stemFactory;

        public GenericPlantPartFactory(GenericStemPartFactory stemFactory)
        {
            this.stemFactory = stemFactory;
        }

        public Stem CreateStem(Internode internode, int branchCount)
        {
            return new GenericStem(internode, branchCount);
        }

        public Internode CreateInternode(float radius)
        {
            int r = (int) radius;

            var cells = cellCreator.CreateCell(r, 0, 0);

            return new GenericInternode(cells, null);
        }

        public Internode CreateInternode(float radius, Node lowerNode)
        {
            int r = (int) radius;

            var at = FindNodeAtHeight(lowerNode);

            var cells = cellCreator.CreateCell(r, 0, at);

            return new GenericInternode(cells, lowerNode);
        }

        public Node CreateNode(Internode lowerInternode, IEnumerable<Stem> stems, IEnumerable<Petiole> petioles)
        {
        }

        public Node CreateNode(Internode lowerInternode, IPlantPartDescriptor internodeDescriptor, IEnumerable<Stem> stems, IEnumerable<Petiole> petioles)
        {
            return new GenericNode(lowerInternode, stems, petioles);
        }

        public Root CreateRoot(float radius)
        {
            throw new NotImplementedException();
        }

        public Root CreateRoot(float radius, Root connection)
        {
            throw new NotImplementedException();
        }

        public Petiole CreatePetiole(Node node)
        {
            throw new NotImplementedException();
        }

        private static ICellGeometry CreateGeometry(IPlantPartDescriptor descriptor, float newY)
        {
            var b = new Vector3(descriptor.Top.X, newY, descriptor.Top.Z);
            var t = new Vector3(b.X, b.Y, b.Z);
            return new CellGeometry(b, t, new Face(new Vector2[0]));
        }

        private static float FindNodeAtHeight(IPlantPart node)
        {
            float max = float.MinValue;

            foreach (var cell in node.Cells)
            {
                var y = cell.Geometry.TopCenter.Y;
                if (y > max)
                {
                    max = y;
                }
            }

            return max;
        }
    }*/
}