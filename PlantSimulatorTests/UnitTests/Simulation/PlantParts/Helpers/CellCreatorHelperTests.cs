using NUnit.Framework;
using PlantSimulator.Simulation.Cells;
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
            var plant = creator.CreateCell(10);

            foreach (var a in plant)
            {
                foreach (var b in plant)
                {
                    if(a.Equals(b)) continue;
                    var collide = collisionDetection.Colliding(a, b, true);
                    Assert.False(collide);
                }
            }

            var length = plant.Count;
            Assert.AreEqual(247, length);

            IPlantCell xylem = null;

            foreach (var c in plant)
            {
                if (c.CellType == PlantCellType.Xylem)
                {
                    xylem = c;
                    break;
                }
            }

            Assert.NotNull(xylem);
            Assert.AreEqual(PlantCellType.Xylem, xylem.CellType);
        }

    }
}