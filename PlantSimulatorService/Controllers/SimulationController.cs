using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Context;
using PlantSimulatorService.Simulations;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SimulationController : ControllerBase
    {
        private readonly ISimulationContext context;

        public SimulationController(ISimulationContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetRunSimulations(CancellationToken cancellationToken)
        {
            return await context.GetSimulations();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRunSimulation([FromRoute] string id, CancellationToken cancellationToken)
        {
            return await context.GetSimulation(id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRunSimulation([FromRoute] string id)
        {
            return context.DeleteSimulation(id);
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartSimulation([FromBody] SimulationOptions options)
        {
            return await context.StartSimulation(options);
        }

    }
}