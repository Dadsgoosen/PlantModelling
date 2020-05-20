using System;
using Grpc.Core;
using Grpc.Net.Client;
using PlantSimulatorService.Simulations.Protos;

namespace PlantSimulatorService.Simulations.Clients
{
    public class SimulationClientFactory : IClientFactory
    {
        public ISimulationClient CreateClient(string ip)
        {
            var host = "https://" + ip;
            return new SimulationClient
            {
                Id = Guid.NewGuid().ToString(),
                Host = host,
                Client = new SimulationServerService.SimulationServerServiceClient(GrpcChannel.ForAddress(host))
            };
        }
    }
}