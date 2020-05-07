using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.Options;
using PlantSim = PlantSimulator.Simulation.PlantSimulator;

namespace PlantSimulatorTests.UnitTests.Simulation
{
    [TestFixture]
    public class PlantSimulatorTests
    {
        private Mock<ILoggerAdapter<PlantSim>> logger;

        private Mock<IPlantSimulatorOptions> options;
        
        private Mock<IPlantRunner> runner;

        private PlantSim plantSimulator;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILoggerAdapter<PlantSim>>();
            options = new Mock<IPlantSimulatorOptions>();
            runner = new Mock<IPlantRunner>();
            plantSimulator = new PlantSim(logger.Object, options.Object, runner.Object);

        }

        [Test]
        public async Task StartAsync_StartingStopping_ShouldStartAndStop()
        {
            options.SetupGet(simulationOptions => simulationOptions.Simulation).Returns(new SimulationOptions{TickTime = 1, TickEventTime = 1});

            Task task = plantSimulator.StartAsync(CancellationToken.None);

            await plantSimulator.StopAsync();

            await task;

            Assert.True(true);
        }


    }
}