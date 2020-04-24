using System;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
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

        private DateTime lastExecutionTime;

        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger, IOptionsMonitor<SimulationOptions> options)
        {
            this.logger = logger;
            this.options = options.CurrentValue;
            lastExecutionTime = DateTime.Now;
        }

        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger, SimulationOptions options)
        {
            this.logger = logger;
            this.options = options;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogDebug("Starting Plant Simulator");
            Stopping = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await Task.Factory.StartNew(Action, cancellationToken, TaskCreationOptions.LongRunning);
        }

        private void Action(object obj)
        {
            logger.LogDebug("Action started");

            while (true)
            {
                if (Stopping.IsCancellationRequested) break;

                if (ShouldSkip()) continue;

                Tick();
                TickCount++;
                lastExecutionTime = DateTime.Now;
            }

            GetType().GetProperties(BindingFlags.CreateInstance | BindingFlags.DeclaredOnly);

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
            var tickTime = options.TickTime;
            if (tickTime <= 0) return false;
            return DateTime.Now.Subtract(lastExecutionTime).TotalMilliseconds < tickTime;
        }

        public void Dispose()
        {
            Stopping?.Cancel();
        }
    }
}