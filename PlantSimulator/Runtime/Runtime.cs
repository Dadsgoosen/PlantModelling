using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;

namespace PlantSimulator.Runtime
{
    public class Runtime : IRuntime
    {
        private readonly ILoggerAdapter<Runtime> logger;

        private readonly ISimulator simulator;

        public Runtime(ILoggerAdapter<Runtime> logger, ISimulator simulator)
        {
            this.logger = logger;
            this.simulator = simulator;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Runtime is starting");

            await simulator.StartAsync(cancellationToken);
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Runtime is stopping");

            simulator.Dispose();

            return Task.CompletedTask;
        }
    }
}