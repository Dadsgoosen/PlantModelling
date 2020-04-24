using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Context
{
    public interface ISimulationContext
    {
        public Task<IActionResult> GetSimulations();

        public Task<IActionResult> GetSimulation(string id);

        public IActionResult DeleteSimulation(string id);

        public Task<IActionResult> StartSimulation(SimulationOptions options);
    }
}