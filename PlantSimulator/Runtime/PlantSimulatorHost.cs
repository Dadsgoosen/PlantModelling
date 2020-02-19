using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;

namespace PlantSimulator.Runtime
{
    public class PlantSimulatorHost : IHostedService
    {
        private readonly IAsyncRuntime runtime;

        private readonly ILoggerAdapter<PlantSimulatorHost> logger;

        private readonly IHostApplicationLifetime applicationLifetime;

        public PlantSimulatorHost(IAsyncRuntime runtime, ILoggerAdapter<PlantSimulatorHost> logger, IHostApplicationLifetime applicationLifetime)
        {
            this.runtime = runtime;
            this.logger = logger;
            this.applicationLifetime = applicationLifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("Hosting is starting");

            applicationLifetime.ApplicationStarted.Register(runtime.OnStartedAsync);
            applicationLifetime.ApplicationStopping.Register(runtime.OnStoppingAsync);
            applicationLifetime.ApplicationStopped.Register(runtime.OnStoppedAsync);

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("Host is stopping");

            return Task.CompletedTask;
        }
    }
}