using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using NUnit.Framework;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Corn;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulatorTests.UnitTests.Simulation.PlantParts.Helpers
{
    [TestFixture]
    public class CellCreatorHelperTests
    {
        private IPlantPartCellCreator creator;

        private ICellCollisionDetection collisionDetection;

        private IEnumerable<IPlantCell> plant;

        [SetUp]
        public void SetUp()
        {
            var cellFactory = new GenericCellFactory();

            var cellCreator = new HexagonCellCreator(cellFactory);

            var cellTypeLocator = CornCellTypeLocator.GetCornCellTypeLocator();

            var optionsService = PlantSimulatorOptionsHelper.CreateOptionsService();

            var cellGridCreator = new HexagonalCellGridFactory(cellCreator, cellTypeLocator, optionsService);

            creator = new PlantPartCellCreator(cellGridCreator);

            var internodeCells = creator.CreateCells(21, 17, new Vector3(0), 0f);

            Internode internode = new GenericInternode(internodeCells, 0);

            plant = internode.Cells;

            collisionDetection = new CellCollisionDetection(new GeometryHelper());
        }


        [Test]
        public void CreateCell_WhenCreatingCornPlant_ShouldHaveCorrectStructure()
        {
            foreach (var a in plant)
            {
                foreach (var b in plant)
                {
                    if(a.Equals(b)) continue;
                    var collide = collisionDetection.Colliding(a, b, true);
                    Assert.False(collide);
                }
            }

            var length = plant.Count();

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