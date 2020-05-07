using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PlantSimulatorService.Simulations;
using PlantSimulatorService.Simulations.Clients;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Context
{
    public class SimulationContext : ISimulationContext
    {
        private readonly IClientHandler clientHandler;

        private readonly ISimulationStorage storage;

        private int i = 0;

        public SimulationContext(IClientHandler clientHandler, ISimulationStorage storage)
        {
            this.clientHandler = clientHandler;
            this.storage = storage;
        }

        public async Task<IActionResult> GetSimulations()
        {
            await storage.StoreSimulationAsync(new SimulationState
            {
                Id = i.ToString(),
                Date = DateTime.Now,
                SimulationTime = 0,
                Plant = new PlantModel
                {
                    RootSystem = new PlantNodeModel[0],
                    ShootSystem = new PlantNodeModel[0]
                }
            });

            i++;

            return new OkObjectResult(await storage.GetSimulationStates());
        }

        public async Task<IActionResult> GetSimulation(string id)
        {
            try
            {
                return new OkObjectResult(await storage.GetSimulationState(id));
            }
            catch (FileNotFoundException)
            {
                return new NotFoundObjectResult($"No simulation with id {id} was found");
            }
        }

        public IActionResult DeleteSimulation(string id)
        {
            var success = storage.DeleteSimulationState(id);

            if(success) return new OkResult();

            return new NotFoundResult();
        }

        public async Task<IActionResult> StartSimulation(SimulationOptions options)
        {
            try
            {
                await clientHandler.StartAvailableClientAsync(options);
            }
            catch (NoAvailableClientsException e)
            {
                return new BadRequestObjectResult(e.Message);
            } 

            return new OkResult();
        }
    }
}