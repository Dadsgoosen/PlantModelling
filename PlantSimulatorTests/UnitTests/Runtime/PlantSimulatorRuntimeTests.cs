using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;

namespace PlantSimulatorTests.UnitTests.Runtime
{
    [TestFixture]
    public class PlantSimulatorRuntimeTests
    {
        private Mock<ILoggerAdapter<PlantSimulator.Runtime.PlantSimulatorRuntime>> logger;

        private PlantSimulator.Runtime.PlantSimulatorRuntime simulator;


        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILoggerAdapter<PlantSimulator.Runtime.PlantSimulatorRuntime>>();
            simulator = new PlantSimulator.Runtime.PlantSimulatorRuntime(logger.Object);
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