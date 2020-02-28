using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using PlantSimulator.Logging;

namespace PlantSimulator.Simulation
{
    public class PlantSimulator : ISimulator
    {
        private readonly ILoggerAdapter<PlantSimulator> logger;

        private readonly SimulationOptions options;

        private CancellationTokenSource Stopping { get; set; }

        public ulong TickCount { get; private set; }

        public Thread Thread;

        private DateTime lastExecutionTime;

        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger, IOptions<SimulationOptions> options)
        {
            this.logger = logger;
            this.options = options.Value;
            lastExecutionTime = DateTime.Now;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("Starting Plant Simulator");
            Stopping = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await Task.Factory.StartNew(Action, cancellationToken, TaskCreationOptions.LongRunning);
        }

        private void Action(object obj)
        {
            logger.LogDebug("Action is started");

            while (true)
            {
                if (Stopping.IsCancellationRequested)
                {
                    break;
                }

                if (ShouldSkip())
                {
                    continue;
                }

                Tick();
                TickCount++;
                lastExecutionTime = DateTime.Now;
            }

            logger.LogDebug("Action has ended");
        }

        private void Tick()
        {
            logger.LogInformation("Ticking");
        }

        public Task StopAsync()
        {
            logger.LogDebug("Stopping the Plant Simulator");
            
            Dispose();

            return Task.CompletedTask;
        }

        private bool ShouldSkip()
        {
            if (options.TickTime <= 0) return false;
            return DateTime.Now.Subtract(lastExecutionTime).TotalMilliseconds < options.TickTime;
        }

        public void Dispose()
        {
            Stopping?.Cancel();
        }
    }
}