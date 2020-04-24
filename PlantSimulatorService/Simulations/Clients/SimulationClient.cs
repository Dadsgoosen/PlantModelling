using System.Threading.Tasks;
using Grpc.Core;
using Grpc.Net.Client;
using PlantSimulatorService.Simulations.Model;
using PlantSimulatorService.Simulations.Protos;

namespace PlantSimulatorService.Simulations.Clients
{
    public class SimulationClient : ISimulationClient
    {
        public string Id { get; set; }

        public bool Available { get; private set; }

        public SimulationServerService.SimulationServerServiceClient Client { get; set; }

        public SimulationClient()
        {
            Available = true;
        }

        public async Task StartAsync(SimulationOptions options)
        {
            var conf = new SimulationConfiguration
            {
                Id = options.Id,
                TickTime = options.TickTime
            };

            try
            {
                await Client.StartSimulationAsync(conf);
                Available = false;
            }
            catch (RpcException e)
            {
                Available = true;
            }
        }

        public async Task StopAsync()
        {
            Available = true;
            await Client.StopSimulationAsync(new StopSimulationRequest());
        }
    }
}