using System.Threading;
using System.Threading.Tasks;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;

namespace PlantSimulatorTests.UnitTests.Runtime
{
    [TestFixture]
    public class RuntimeTests
    {
        private Mock<ILoggerAdapter<PlantSimulator.Runtime.Runtime>> logger;

        private PlantSimulator.Runtime.Runtime runtime;

        private Mock<ISimulator> simulator;

        [SetUp]
        public void Setup()
        {
            logger = new Mock<ILoggerAdapter<PlantSimulator.Runtime.Runtime>>();

            simulator = new Mock<ISimulator>();

            runtime = new PlantSimulator.Runtime.Runtime(logger.Object, simulator.Object);
        }

        [Test]
        public async Task OnStartedAsync_Starting_ShouldStartSimulator()
        {
            await runtime.StartAsync(CancellationToken.None);
            simulator.Verify(sim => sim.StartAsync(CancellationToken.None));
        }

        [Test]
        public void OnStoppedAsync_Stopping_ShouldDisposeSimulator()
        {
            runtime.StopAsync(CancellationToken.None);
            simulator.Verify(sim => sim.Dispose());
        }
    }
}