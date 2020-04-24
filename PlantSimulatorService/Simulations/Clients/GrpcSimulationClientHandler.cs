using System.Threading.Tasks;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Simulations.Clients
{
    public class GrpcSimulationClientHandler : IClientHandler
    {
        private readonly IClientCollection clients;

        public GrpcSimulationClientHandler(IClientCollection clients)
        {
            this.clients = clients;
        }

        /// <summary>
        /// Send the simulation task to an available client
        /// </summary>
        /// <param name="options">Simulation options to start the simulation with</param>
        /// <exception cref="NoAvailableClientsException">Thrown if there is no client available to take the task</exception>
        public async Task StartAvailableClientAsync(SimulationOptions options)
        {
            foreach (ISimulationClient client in clients)
            {
                if (!client.Available)
                {
                    continue;
                }

                await client.StartAsync(options);

                return;
            }

            throw new NoAvailableClientsException("No clients are available to take the task");
        }
    }
}