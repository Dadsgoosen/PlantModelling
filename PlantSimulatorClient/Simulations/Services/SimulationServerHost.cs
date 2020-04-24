using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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

            ServerHelloResponse reply = await client.SayHelloAsync(new ServerHelloRequest {Ip = ipOptions.Ip});

            Id = reply.Id;

            logger.LogInformation("Received Id " + Id);
        }

        public async Task StopAsync(CancellationToken cancellationToken)
        {
            logger.LogInformation("Saying Goodbye");

            await client.SayGoodByeAsync(new ServerGoodByeRequest{Id = Id});
        }
    }
}