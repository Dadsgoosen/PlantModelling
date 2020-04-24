using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using PlantSimulator.Logging;

namespace PlantSimulator.Runtime
{
    public class PlantSimulatorHost : IHostedService
    {
        private readonly ILoggerAdapter<PlantSimulatorHost> logger;

        private readonly IRuntime runtime;

        private readonly IHostApplicationLifetime lifetime;

        public PlantSimulatorHost(ILoggerAdapter<PlantSimulatorHost> logger, IRuntime runtime, IHostApplicationLifetime lifetime)
        {
            this.logger = logger;
            this.runtime = runtime;
            this.lifetime = lifetime;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Host is starting");

            lifetime.ApplicationStarted.Register(() => runtime.StartAsync(cancellationToken));
            lifetime.ApplicationStopping.Register(() => runtime.StopAsync(cancellationToken));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Host is stopping");

            return Task.CompletedTask;
        }
    }
}