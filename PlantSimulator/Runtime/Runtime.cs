﻿using System.Threading;
using System.Threading.Tasks;
using PlantSimulator.Logging;
using PlantSimulator.Simulation;

namespace PlantSimulator.Runtime
{
    public class Runtime : IRuntime
    {
        private readonly ILoggerAdapter<Runtime> logger;

        public Runtime(ILoggerAdapter<Runtime> logger)
        {
            this.logger = logger;
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Runtime is starting");

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Runtime is stopping");

            return Task.CompletedTask;
        }
    }
}