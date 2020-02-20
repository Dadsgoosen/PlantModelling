using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;
using Serilog;

namespace PlantSimulator.Runtime
{
    public class PlantSimulatorRuntime : IAsyncRuntime
    {
        private readonly ILoggerAdapter<PlantSimulatorRuntime> logger;

        private readonly ISimulator simulator;

        public PlantSimulatorRuntime(ILoggerAdapter<PlantSimulatorRuntime> logger, ISimulator simulator)
        {
            this.logger = logger;
            this.simulator = simulator;
        }

        public void OnStartedAsync()
        {
            logger.LogInformation("Starting the simulation");
            simulator.Start();
        }

        public void OnStoppingAsync()
        {
            logger.LogInformation("Stopping the simulation gracefully");
            simulator.Stop();
        }

        public void OnStoppedAsync()
        {
            logger.LogInformation("The simulation has stopped");
        }
    }
}