using System;
using Grpc.Core;
using Grpc.Net.Client;
using PlantSimulatorService.Simulations.Protos;

namespace PlantSimulatorService.Simulations.Clients
{
    public class SimulationClientFactory : IClientFactory
    {
        public ISimulationClient CreateClient(ServerHelloRequest request, ServerCallContext context)
        {
            string host = "https://" + request.Ip;
            return new SimulationClient
            {
                Id = Guid.NewGuid().ToString(),
                Host = host,
                Client = new SimulationServerService.SimulationServerServiceClient(GrpcChannel.ForAddress(host))
            };
        }
    }
}