using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Simulations.Clients;

namespace PlantSimulatorService.Context
{
    public class ClientContext : IClientContext
    {
        private readonly IClientCollection clients;

        public ClientContext(IClientCollection clients)
        {
            this.clients = clients;
        }

        public IActionResult GetClients()
        {
            return new OkObjectResult(clients.GetClients());
        }

        public async Task<IActionResult> StopClient(string id)
        {
            var client = clients.GetClient(id);

            if (client == null)
            {
                return new NotFoundResult();
            }

            await client.StopAsync();

            return new OkResult();
        }
    }
}