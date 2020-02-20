using System;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Moq;
using NUnit.Framework;
using PlantSimulator.Logging;
using PlantSimulator.Runtime;

namespace PlantSimulatorTests.UnitTests.Runtime
{
    public class PlantSimulatorHostTests
    {
        private Mock<IRuntime> runtime;

        private Mock<ILoggerAdapter<PlantSimulatorHost>> loggerAdapter;

        private Mock<IHostApplicationLifetime> lifetime;

        private IHostedService hostedService;

        [SetUp]
        public void Setup()
        {
            runtime = new Mock<IRuntime>();
            loggerAdapter = new Mock<ILoggerAdapter<PlantSimulatorHost>>();
            lifetime = new Mock<IHostApplicationLifetime>();
            hostedService = new PlantSimulatorHost(loggerAdapter.Object, runtime.Object, lifetime.Object);
        }

        [Test]
        public async Task StartAsync_RegisterRunTime_ShouldRegisterAllLifetimeCallbacks()
        {
            await hostedService.StartAsync(CancellationToken.None);

            lifetime.VerifyGet(x => x.ApplicationStarted, Times.Once);
            lifetime.VerifyGet(x => x.ApplicationStopping, Times.Once);
        }

    }
}