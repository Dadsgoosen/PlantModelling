using System.Threading.Tasks;
using Grpc.Core;
using PlantSimulatorService.Context;
using PlantSimulatorService.Simulations.Model;
using PlantSimulatorService.Simulations.Protos;
using SimulationState = PlantSimulatorService.Simulations.Protos.SimulationState;

namespace PlantSimulatorService.Simulations.Services
{
    public class SimulationClientService : Protos.SimulationClientService.SimulationClientServiceBase
    {
        private readonly ISimulationClientContext simulationContext;

        public SimulationClientService(ISimulationClientContext simulationContext)
        {
            this.simulationContext = simulationContext;
        }

        public override Task<ServerHelloResponse> SayHello(ServerHelloRequest request, ServerCallContext context)
        {
            var client = simulationContext.SayHello(request.Ip);
            return Task.FromResult(new ServerHelloResponse {Id = client.Id});
        }

        public override Task<ServerGoodByeResponse> SayGoodBye(ServerGoodByeRequest request, ServerCallContext context)
        {
            simulationContext.SayGoodbye(request.Id);
            return Task.FromResult(new ServerGoodByeResponse());
        }

        public override async Task<SimulationStateResponse> TransmitState(SimulationState request, ServerCallContext context)
        {
            await simulationContext.TransmitState(request.ToPlantModel(), context.CancellationToken);
            return new SimulationStateResponse();
        }
    }
}