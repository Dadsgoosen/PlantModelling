using System;
using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Logging;
using PlantSimulator.Runtime.Helpers;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Runtime
{
    public class SimulationRuntimeBroker : IRuntimeBroker<Simulation.PlantSimulator>
    {
        public RuntimeStatus Status => runningSimulation != null ? RuntimeStatus.Running : RuntimeStatus.Waiting;

        private Task runningSimulation;
        public Simulation.PlantSimulator Simulation { get; private set; }

        private CancellationTokenSource cancellationTokenSource;

        private readonly ILoggerAdapter<SimulationRuntimeBroker> logger;

        private readonly ISimulatorEventHandler eventHandler;

        private readonly IServiceProvider provider;

        public SimulationRuntimeBroker(ILoggerAdapter<SimulationRuntimeBroker> logger, ISimulatorEventHandler eventHandler, IServiceProvider provider)
        {
            this.logger = logger;
            this.eventHandler = eventHandler;
            this.provider = provider;
        }

        public Task StartSimulationAsync(PlantSimulationOptions options, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting simulator from runtime broker");

            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            Simulation = provider.ResolvePlantSimulatorConstructor(options);

            Simulation.OnTick += eventHandler.OnSimulationTick;

            try
            {
                runningSimulation = Simulation.StartAsync(cancellationTokenSource.Token);
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

            Simulation.OnTick -= eventHandler.OnSimulationTick;

            runningSimulation = null;
            Simulation = null;
        }
    }
}