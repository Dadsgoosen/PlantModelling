using System;
using System.Collections.Generic;
using NUnit.Framework;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulatorTests.UnitTests.Simulation.Geometry
{
    [TestFixture]
    public class VertexTests
    {
        [Test]
        public void GetHashCode_MultipleHashCodes_ShouldBeUnique()
        {
            const int amount = 100;

            var vertices = new HashSet<IVertex>(1_000_000);

            for (var x = 0; x < amount; x++)
            {
                for (var y = 0; y < amount; y++)
                {
                    for (var z = 0; z < amount; z++)
                    {
                        var vertex = new Vertex(x, y, z);
                        
                        if (vertices.TryGetValue(vertex, out var vert))
                        {
                            Assert.True(false, $"There already exists the coordinate {vert})");
                        }
                        
                        vertices.Add(vertex);
                    }
                }
            }

            vertices.Clear();
        }

        [Test]
        public void GetHashCode_SameVertices_ShouldBeUnique()
        {
            var vert1 = new Vertex(0, 0, 0);
            var vert2 = new Vertex(0, 0, 0);
            Assert.AreNotEqual(vert1, vert2);
            Assert.AreNotEqual(vert1.GetHashCode(), vert2.GetHashCode());
        }
    }
}