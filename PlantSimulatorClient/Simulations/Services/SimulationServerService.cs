using System;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using PlantSimulator.Logging;
using PlantSimulator.Runtime;
using PlantSimulator.Simulation;
using PlantSimulatorClient.Simulations.Protos;

namespace PlantSimulatorClient.Simulations.Services
{
    public class SimulationServerService : Protos.SimulationServerService.SimulationServerServiceBase
    {
        private readonly IRuntimeBroker<PlantSimulator.Simulation.PlantSimulator> broker;

        private readonly ILoggerAdapter<SimulationServerService> logger;

        public SimulationServerService(IRuntimeBroker<PlantSimulator.Simulation.PlantSimulator> broker, ILoggerAdapter<SimulationServerService> logger)
        {
            this.broker = broker;
            this.logger = logger;
        }

        public override async Task<StartSimulationResponse> StartSimulation(SimulationConfiguration request, ServerCallContext context)
        {
            try
            {
                if (broker.Status == RuntimeStatus.Waiting)
                {
                    await broker.StartSimulationAsync(request.ToSimulationOptions(),
                        new CancellationTokenSource().Token);
                }
            }
            catch (NullReferenceException e)
            {
                logger.LogError(e, "The simulation {Id} could not be started", request.Id);
            }
            return new StartSimulationResponse();
        }

        public override async Task<StopSimulationResponse> StopSimulation(StopSimulationRequest request, ServerCallContext context)
        {
            if (broker.Status == RuntimeStatus.Running)
            {
                await broker.StopSimulationAsync();
            }

            return new StopSimulationResponse();
        }
    }
}