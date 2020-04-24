using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace PlantSimulatorService.Context
{
    public interface IClientContext
    {
        public IActionResult GetClients();

        public Task<IActionResult> StopClient(string id);
    }
}