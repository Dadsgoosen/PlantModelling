using System;
using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Logging;
using PlantSimulator.Runtime.Helpers;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Runtime
{
    public class SimulationRuntimeBroker : IRuntimeBroker
    {
        public RuntimeStatus Status => runningSimulation != null ? RuntimeStatus.Running : RuntimeStatus.Waiting;

        private Task runningSimulation;

        private CancellationTokenSource cancellationTokenSource;

        private readonly ILoggerAdapter<SimulationRuntimeBroker> logger;

        private readonly IServiceProvider provider;

        public SimulationRuntimeBroker(ILoggerAdapter<SimulationRuntimeBroker> logger, IServiceProvider provider)
        {
            this.logger = logger;
            this.provider = provider;
        }

        public Task StartSimulationAsync(SimulationOptions options, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting simulator from runtime broker");

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            var simulation = provider.ResolvePlantSimulatorConstructor(options);

            try
            {
                runningSimulation = simulation.StartAsync(cancellationTokenSource.Token);
            }
            catch (NullReferenceException e)
            {
                throw new NullReferenceException("The simulation could not be constructed by the service provider", e);
            }

            return Task.CompletedTask;
        }

        public async Task StopSimulationAsync(CancellationToken cancellationToken = default)
        {
            logger.LogInformation("Stopping simulator from runtime broker");
            
            cancellationTokenSource.Cancel();

            await runningSimulation;

            runningSimulation = null;
        }
    }
}