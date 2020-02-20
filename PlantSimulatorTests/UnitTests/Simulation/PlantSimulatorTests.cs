using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSim = PlantSimulator.Simulation.PlantSimulator;

namespace PlantSimulatorTests.UnitTests.Simulation
{
    [TestFixture]
    public class PlantSimulatorTests
    {
        private Mock<ILoggerAdapter<PlantSim>> logger;

        private PlantSim plantSimulator;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILoggerAdapter<PlantSim>>();
            plantSimulator = new PlantSim(logger.Object);
        }

        [Test]
        public async Task StartAsync_StartingStopping_ShouldStartAndStop()
        {
            Task task = plantSimulator.StartAsync(CancellationToken.None);

            await plantSimulator.StopAsync();

            await task;

            Assert.True(true);
        }


    }
}