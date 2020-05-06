using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PlantSimulator.Logging;
using PlantSimulatorClient.Simulations.Protos;

namespace PlantSimulatorClient.Simulations.Services
{
    internal class SimulationServerHost : IHostedService
    {
        private readonly SimulationIpOption ipOptions;

        private readonly ILoggerAdapter<SimulationServerHost> logger;

        private readonly SimulationClientService.SimulationClientServiceClient client;

        private string Id { get; set; }

        public SimulationServerHost(
            SimulationIpOption ipOptions,
            ILoggerAdapter<SimulationServerHost> logger, 
            SimulationClientService.SimulationClientServiceClient client)
        {
            this.ipOptions = ipOptions;
            this.logger = logger;
            this.client = client;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Sending Hello Message to Server");

            var response = await SayHello();

            Id = response.Id;

            logger.LogInformation("Received Id " + Id);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Saying Goodbye");

            await client.SayGoodByeAsync(new ServerGoodByeRequest{Id = Id});
        }

        private async Task<ServerHelloResponse> SayHello()
        {
            ServerHelloResponse response = null;

            do
            {
                try
                {
                    response = await client.SayHelloAsync(new ServerHelloRequest { Ip = ipOptions.Ip });
                }
                catch (RpcException e)
                {
                    logger.LogWarning(e, "Could not say hello to server, retrying...");
                }
            } while (response == null);

            return response;
        }
    }
}