using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using PlantSimulator.Runtime;
using PlantSimulator.Simulation;
using PlantSimulatorClient.Simulations.Protos;

namespace PlantSimulatorClient.Simulations.Services
{
    public class SimulationServerService : Protos.SimulationServerService.SimulationServerServiceBase
    {
        private readonly IRuntimeBroker<PlantSimulator.Simulation.PlantSimulator> broker;

        public SimulationServerService(IRuntimeBroker<PlantSimulator.Simulation.PlantSimulator> broker)
        {
            this.broker = broker;
        }

        public override async Task<StartSimulationResponse> StartSimulation(SimulationConfiguration request, ServerCallContext context)
        {
            if (broker.Status == RuntimeStatus.Waiting)
            {
                await broker.StartSimulationAsync(request.ToSimulationOptions(), new CancellationTokenSource().Token);
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