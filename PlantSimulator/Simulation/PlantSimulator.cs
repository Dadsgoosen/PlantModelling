using System;
using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Logging;

namespace PlantSimulator.Simulation
{
    public class PlantSimulator : ISimulator
    {
        public ILoggerAdapter<PlantSimulator> Logger { get; }

        public CancellationTokenSource Stopping { get; }

        public ulong Ticker { get; private set; }

        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger)
        {
            Logger = logger;
            Stopping = new CancellationTokenSource();
        }

        public void Start()
        {
            Logger.LogDebug("Starting Plant Simulator");
            Thread start = new Thread(Action);
            start.Start();
        }

        private void Action()
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
                Ticker++;
            }

            Logger.LogDebug("Action has ended");
        }

        private void Tick()
        {
            Logger.LogInformation("Ticking");
        }

        public void Stop()
        {
            Logger.LogDebug("Stopping the Plant Simulator");
            Stopping.Cancel();
            Dispose();
        }

        public void Dispose()
        {
            Stopping?.Dispose();
        }
    }
}