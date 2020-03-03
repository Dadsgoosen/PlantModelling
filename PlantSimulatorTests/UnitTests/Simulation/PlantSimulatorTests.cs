using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;
using PlantSim = PlantSimulator.Simulation.PlantSimulator;

namespace PlantSimulatorTests.UnitTests.Simulation
{
    [TestFixture]
    public class PlantSimulatorTests
    {
        private Mock<ILoggerAdapter<PlantSim>> logger;

        private Mock<IOptions<SimulationOptions>> options;

        private PlantSim plantSimulator;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILoggerAdapter<PlantSim>>();
            options = new Mock<IOptions<SimulationOptions>>();
            options.Setup(opt => opt.Value).Returns(new SimulationOptions());
            plantSimulator = new PlantSim(logger.Object, options.Object);

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