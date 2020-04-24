using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;

namespace PlantSimulator.Runtime
{
    public class SimulationRuntimeBroker : IRuntimeBroker
    {
        private readonly ILoggerFactory loggerFactory;

        public RuntimeStatus Status => runningSimulation != null ? RuntimeStatus.Running : RuntimeStatus.Waiting;

        private Task runningSimulation;

        private CancellationTokenSource cancellationTokenSource;

        private readonly ILoggerAdapter<SimulationRuntimeBroker> logger;

        public SimulationRuntimeBroker(ILoggerAdapter<SimulationRuntimeBroker> logger, ILoggerFactory loggerFactory)
        {
            this.logger = logger;
            this.loggerFactory = loggerFactory;
        }

        public Task StartSimulationAsync(SimulationOptions options, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting simulator from runtime broker");

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            var simulation = InstantiateSimulator(options);

            runningSimulation = simulation.StartAsync(cancellationTokenSource.Token);

            return Task.CompletedTask;
        }

        public async Task StopSimulationAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Stopping simulator from runtime broker");
            
            cancellationTokenSource.Cancel();

            await runningSimulation;

            runningSimulation = null;
        }

        private Simulation.PlantSimulator InstantiateSimulator(SimulationOptions options)
        {
            return new Simulation.PlantSimulator(CreateLoggerAdapter<Simulation.PlantSimulator>(), options);
        }

        private ILoggerAdapter<T> CreateLoggerAdapter<T>()
        {
            var logger = loggerFactory.CreateLogger<T>();
            return new LoggerAdapter<T>(logger);
        }
    }
}