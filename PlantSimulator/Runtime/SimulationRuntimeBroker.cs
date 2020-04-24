using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;
using PlantSimulator = PlantSimulator.Simulation.PlantSimulator;

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

            var simulation = InstantiateSimulator(options);

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

        private Simulation.PlantSimulator InstantiateSimulator(SimulationOptions options)
        {
            var type = typeof(Simulation.PlantSimulator);

            InvalidOperationException exception = null;

            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    var parameters = constructor.GetParameters();

                    object[] parameterValues = new object[parameters.Length];

                    for (int i = 0; i < parameters.Length; i++)
                    {
                        var parameter = parameters[i];

                        if (parameter.ParameterType == options.GetType())
                        {
                            parameterValues[i] = options;
                        }
                        else
                        {
                            parameterValues[i] = provider.GetRequiredService(parameter.ParameterType);
                        }
                    }
                    return (Simulation.PlantSimulator) Activator.CreateInstance(type, parameterValues, null);
                }
                catch (InvalidOperationException e)
                {
                    exception = e;
                }
            }

            if (exception != null) throw exception;
            return null;
        }
    }
}