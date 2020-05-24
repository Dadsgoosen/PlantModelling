using System.Numerics;
using Moq;
using NUnit.Framework;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Factories;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations;

namespace PlantSimulatorTests.UnitTests.Simulation.Operations
{
    [TestFixture]
    public class GenericCellDividerTests
    {
        private readonly Mock<ICellFactory> cellFactoryMock;

        private readonly ICellDivider divider;

        public GenericCellDividerTests()
        {
            cellFactoryMock = new Mock<ICellFactory>();
            divider = new GenericCellDivider(cellFactoryMock.Object);
        }

        [Test]
        public void Divide_WhenDividingXylemCell_ShouldHalfTheCell()
        {
            var (original, first, second) = CreateXylemCells();
            cellFactoryMock
                .Setup(factory => factory.CreateCell(
                    It.Is<PlantCellType>(type => type == PlantCellType.Xylem),
                    It.Is<ICellGeometry>(geo => geo.TopCenter.Equals(first.Geometry.TopCenter)),
                    It.IsAny<IVacuole>(),
                    It.IsAny<ICellWall>(),
                    It.IsAny<IPlantCell[]>()
                )).Returns(first);

            cellFactoryMock
                .Setup(factory => factory.CreateCell(
                    It.Is<PlantCellType>(type => type == PlantCellType.Xylem),
                    It.Is<ICellGeometry>(geo => geo.TopCenter.Equals(second.Geometry.TopCenter)),
                    It.IsAny<IVacuole>(),
                    It.IsAny<ICellWall>(),
                    It.IsAny<IPlantCell[]>()
                )).Returns(second);


            var splitCell = divider.Divide(original);

            Assert.True(first.CellType == original.CellType);
            Assert.True(second.CellType == original.CellType);

            Assert.True(first.Geometry.Equals(splitCell[0].Geometry));
            Assert.True(second.Geometry.Equals(splitCell[1].Geometry));
        }

        private (IPlantCell original, IPlantCell first, IPlantCell second) CreateXylemCells()
        {
            var (originalGeo, firstGeo, secondGeo) = CreateGeometries();
            
            IPlantCell original = new XylemCell(originalGeo, new IPlantCell[0], null, null);
            IPlantCell first = new XylemCell(firstGeo, new IPlantCell[0], null, null);
            IPlantCell second = new XylemCell(secondGeo, new IPlantCell[0], null, null);

            return (original, first, second);
        }

        private (ICellGeometry original, ICellGeometry first, ICellGeometry second) CreateGeometries()
        {
            var face = new Face(new[]
            {
                new Vector2(0), new Vector2(0)
            });

            var top = new Vector3 { X = 23F, Y = 30F, Z = 10F};
            var bottom = new Vector3 { X = 10F, Y = 12F, Z = 2F };
            var half = new Vector3 { X = 16.5F, Y = 21, Z = 6 };
            ICellGeometry original = new CellGeometry(top, bottom, face);
            ICellGeometry first = new CellGeometry(top, half, face);
            ICellGeometry second = new CellGeometry(half, bottom, face);

            return (original, first, second);
        }
    }
}