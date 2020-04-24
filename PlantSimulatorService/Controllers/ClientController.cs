using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Context;

namespace PlantSimulatorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientController : ControllerBase
    {
        private readonly IClientContext context;

        public ClientController(IClientContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public IActionResult GetClients()
        {
            return context.GetClients();
        }

        [HttpPost("{id}/stop")]
        public async Task<IActionResult> StopSimulation([FromRoute] string id)
        {
            return await context.StopClient(id);
        }
    }
}