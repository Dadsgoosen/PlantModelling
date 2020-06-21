using System;
using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Helpers;
using PlantSimulator.Logging;
using PlantSimulator.Runtime.Helpers;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Runtime
{
    public class SimulationRuntimeBroker : IRuntimeBroker<Simulation.PlantSimulator>
    {
        public RuntimeStatus Status => runningSimulation != null && !runningSimulation.IsCompleted ? RuntimeStatus.Running : RuntimeStatus.Waiting;

        private Task runningSimulation;

        public Simulation.PlantSimulator Simulation { get; private set; }

        private CancellationTokenSource cancellationTokenSource;

        private readonly ILoggerAdapter<SimulationRuntimeBroker> logger;

        private readonly ISimulatorEventHandler eventHandler;

        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly IServiceProvider provider;

        public SimulationRuntimeBroker(ILoggerAdapter<SimulationRuntimeBroker> logger, 
            ISimulatorEventHandler eventHandler, 
            IPlantSimulatorOptionsService optionsService, 
            IServiceProvider provider)
        {
            this.logger = logger;
            this.eventHandler = eventHandler;
            this.optionsService = optionsService;
            this.provider = provider;
        }

        public Task StartSimulationAsync(PlantSimulationOptions options, CancellationToken cancellationToken)
        {
            logger.LogInformation("Starting simulator from runtime broker");

            options = (PlantSimulationOptions) GenericOptions.CreateOptions();

            // Set the random seed from the options
            RangeExtensions.Random = new Random(options.Simulation.RandomSeed);

            // Set the options service options to the received options
            optionsService.Options = options;

            // Linked cancellation token source so that the simulation can be cancelled
            cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);

            // Instantiate the simulation instance with the dependency injection service container
            Simulation = provider.ResolvePlantSimulatorConstructor();

            // Add event subscribers to the OnTick event
            Simulation.OnTick += eventHandler.OnSimulationTick;

            // Start the simulation if it was instantiated correctly through dependency injection
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