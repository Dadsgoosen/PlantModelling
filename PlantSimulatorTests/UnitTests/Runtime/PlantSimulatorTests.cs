using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;

namespace PlantSimulatorTests.UnitTests.Runtime
{
    [TestFixture]
    public class PlantSimulatorTests
    {
        private Mock<ILoggerAdapter<PlantSimulator.Runtime.PlantSimulator>> logger;

        private PlantSimulator.Runtime.PlantSimulator simulator;


        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILoggerAdapter<PlantSimulator.Runtime.PlantSimulator>>();
            simulator = new PlantSimulator.Runtime.PlantSimulator(logger.Object);
        }

        [Test]
        public void OnStartedAsync_Starting_ShouldLogStartup()
        {
            simulator.OnStartedAsync();
            logger.Verify(log => log.LogInformation(It.IsAny<string>()));
        }

        [Test]
        public void OnStoppedAsync_Stopped_ShouldLogStopped()
        {
            simulator.OnStoppedAsync();
            logger.Verify(log => log.LogInformation(It.IsAny<string>()));
        }

        [Test]
        public void OnStoppingAsync_Stopping_ShouldLogStopping()
        {
            simulator.OnStoppingAsync();
            logger.Verify(log => log.LogInformation(It.IsAny<string>()));
        }
    }
}