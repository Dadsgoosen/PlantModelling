using System.Threading.Tasks;
using Grpc.Core;
using PlantSimulatorService.Simulations.Clients;
using PlantSimulatorService.Simulations.Model;
using PlantSimulatorService.Simulations.Protos;
using SimulationState = PlantSimulatorService.Simulations.Protos.SimulationState;

namespace PlantSimulatorService.Simulations.Services
{
    public class SimulationClientService : Protos.SimulationClientService.SimulationClientServiceBase
    {
        private readonly IClientCollection clients;

        private readonly IClientFactory clientFactory;

        private readonly ISimulationStorage simulationStorage;

        public SimulationClientService(IClientCollection clients, IClientFactory clientFactory, ISimulationStorage simulationStorage)
        {
            this.clients = clients;
            this.clientFactory = clientFactory;
            this.simulationStorage = simulationStorage;
        }


        public override Task<ServerHelloResponse> SayHello(ServerHelloRequest request, ServerCallContext context)
        {
            var client = clientFactory.CreateClient(request, context);

            clients.AddClient(client);

            return Task.FromResult(new ServerHelloResponse {Id = client.Id});
        }

        public override Task<ServerGoodByeResponse> SayGoodBye(ServerGoodByeRequest request, ServerCallContext context)
        {
            clients.DeleteClient(request.Id);
            return Task.FromResult(new ServerGoodByeResponse());
        }

        public override async Task<SimulationStateResponse> TransmitState(SimulationState request, ServerCallContext context)
        {
            await simulationStorage.StoreSimulationAsync(request.ToPlantModel(), context.CancellationToken);
            return new SimulationStateResponse();
        }
    }
}