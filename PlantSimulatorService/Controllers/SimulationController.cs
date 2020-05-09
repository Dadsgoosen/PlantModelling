using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Context;
using PlantSimulatorService.Simulations.Model.Options;

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
        public async Task<IActionResult> GetRunSimulations()
        {
            return await context.GetSimulations();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetRunSimulation([FromRoute] string id)
        {
            return await context.GetSimulation(id);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteRunSimulation([FromRoute] string id)
        {
            return context.DeleteSimulation(id);
        }

        [HttpPost("start")]
        public async Task<IActionResult> StartSimulation([FromBody] PlantSimulationOptions options)
        {
            return await context.StartSimulation(options);
        }

    }
}