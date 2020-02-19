using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PlantSimulator.Logging;
using Serilog;

namespace PlantSimulator.Runtime
{
    public class PlantSimulator : IAsyncRuntime
    {
        private readonly ILoggerAdapter<PlantSimulator> logger;

        public PlantSimulator(ILoggerAdapter<PlantSimulator> logger)
        {
            this.logger = logger;
        }

        public void OnStartedAsync()
        {
            logger.LogInformation("Starting the simulation");
        }

        public void OnStoppingAsync()
        {
            logger.LogInformation("Stopping the simulation gracefully");
        }

        public void OnStoppedAsync()
        {
            logger.LogInformation("The simulation has stopped");
        }
    }
}