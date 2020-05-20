using System.Threading;
using System.Threading.Tasks;
using PlantSimulatorService.Simulations;
using PlantSimulatorService.Simulations.Clients;
using SimulationState = PlantSimulatorService.Simulations.Model.SimulationState;

namespace PlantSimulatorService.Context
{
    public class SimulationClientContext : ISimulationClientContext
    {
        private readonly IClientCollection clients;

        private readonly IClientFactory clientFactory;

        private readonly ISimulationStorage simulationStorage;

        public SimulationClientContext(IClientCollection clients, IClientFactory clientFactory, ISimulationStorage simulationStorage)
        {
            this.clients = clients;
            this.clientFactory = clientFactory;
            this.simulationStorage = simulationStorage;
        }

        public ISimulationClient SayHello(string ip)
        {
            var client = clientFactory.CreateClient(ip);

            clients.AddClient(client);

            return client;
        }

        public void SayGoodbye(string id)
        {
            clients.DeleteClient(id);
        }

        public async Task TransmitState(SimulationState state, CancellationToken cancellationToken)
        {
            await simulationStorage.StoreSimulationAsync(state, cancellationToken);
        }
    }
}