using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Simulations.Api;

namespace PlantSimulatorService.Controllers
{
    [ApiController]
    [Route("/")]
    public class ServiceController : ControllerBase
    {
        public ISimulationApiInformation ApiInformation { get; }

        public ServiceController(ISimulationApiInformation apiInformation)
        {
            ApiInformation = apiInformation;
        }

        [HttpGet]
        public IActionResult ServiceInformation()
        {
            return Ok(ApiInformation);
        }
    }
}