using NUnit.Framework;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulatorTests.UnitTests.Simulation.PlantParts.Helpers
{
    [TestFixture]
    public class CellCreatorHelperTests
    {
        private ICellCreatorHelper creator;

        private ICellCollisionDetection collisionDetection;

        [SetUp]
        public void SetUp()
        {
            creator = new CellCreatorHelper();
            collisionDetection = new CellCollisionDetection(new GeometryHelper());
        }


        [Test]
        public void CreateCell_WhenCreatingCornPlant_ShouldHaveCorrectStructure()
        {
            var plant = creator.CreateCell();

            foreach (var a in plant)
            {
                foreach (var b in plant)
                {
                    if(a == b) continue;
                    var collide = collisionDetection.Colliding(a, b, true);
                    Assert.False(collide);
                }
            }
        }

    }
}