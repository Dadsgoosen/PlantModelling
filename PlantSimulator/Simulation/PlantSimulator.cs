using System;
using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Logging;

namespace PlantSimulator.Simulation
{
    public class PlantSimulator : ISimulator
    {
        public ILoggerAdapter<PlantSimulator> Logger { get; }

        public CancellationTokenSource Stopping { get; private set; }

        public ulong TickCount { get; private set; }

        public Thread Thread;

        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger)
        {
            Logger = logger;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            Logger.LogDebug("Starting Plant Simulator");
            Stopping = CancellationTokenSource.CreateLinkedTokenSource(cancellationToken);
            await Task.Factory.StartNew(Action, cancellationToken, TaskCreationOptions.LongRunning);
        }

        private void Action(object obj)
        {
            Logger.LogDebug("Action is started");

            while (true)
            {
                if (Stopping.IsCancellationRequested)
                {
                    Logger.LogDebug("Plant Simulator was cancelled");
                    break;
                }

                Tick();
                TickCount++;
            }

            Logger.LogDebug("Action has ended");
        }

        private void Tick()
        {
            Logger.LogInformation("Ticking");
        }

        public Task StopAsync()
        {
            Logger.LogDebug("Stopping the Plant Simulator");
            
            Stopping.Cancel();
            
            Dispose();

            return Task.CompletedTask;
        }

        public void Dispose()
        {
            Stopping?.Dispose();
        }
    }
}