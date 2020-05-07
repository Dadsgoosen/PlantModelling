using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Operations;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Simulation
{
    public class PlantSimulator : ISimulator
    {
        private readonly ILoggerAdapter<PlantSimulator> logger;

        private readonly IPlantRunner plantRunner;

        private readonly PlantSimulationOptions options;

        private CancellationTokenSource Stopping { get; set; }

        public ulong TickCount { get; private set; }

        private DateTime lastExecutionTime;

        public event EventHandler<PlantSimulatorTickEvent> OnTick; 
        
        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger, PlantSimulationOptions options, IPlantRunner plantRunner)
        {
            this.logger = logger;
            this.options = options;
            this.plantRunner = plantRunner;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("Starting Plant Simulator");
            Stopping = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await Task.Factory.StartNew(Action, cancellationToken, TaskCreationOptions.LongRunning);
        }

        private void Action(object obj)
        {
            logger.LogDebug("Action started and transmitting start state");
            
            while (true)
            {
                if (Stopping.IsCancellationRequested) break;

                if (ShouldSkip()) continue;

                Tick();

                if (ShouldSendInvokeTickEvent())
                {
                    InvokeTickEvent();
                }
                
                TickCount++;
                lastExecutionTime = DateTime.Now;
            }

            logger.LogDebug("Action loop has ended and invoking the tick event");

            InvokeTickEvent();

            logger.LogDebug("Action has ended");
        }

        private void Tick()
        {
            logger.LogDebug("Ticking...");

            plantRunner.Tick(new SimulationStateSnapshot(TickCount));
        }

        public Task StopAsync()
        {
            logger.LogDebug("Stopping the Plant Simulator");

            Dispose();

            return Task.CompletedTask;
        }

        private bool ShouldSkip()
        {
            var tickTime = options.Simulation.TickTime;
            if (tickTime <= 0) return false;
            return DateTime.Now.Subtract(lastExecutionTime).TotalMilliseconds < tickTime;
        }

        private bool ShouldSendInvokeTickEvent()
        {
            return TickCount % options.Simulation.TickEventTime == 0;
        }

        private void InvokeTickEvent()
        {
            logger.LogInformation("Invoking {OnTick} event", nameof(OnTick));

            var handler = OnTick;

            handler?.Invoke(this, new PlantSimulatorTickEvent(plantRunner.Plant, TickCount));
        }

        public void Dispose()
        {
            Stopping?.Cancel();
        }
    }
}