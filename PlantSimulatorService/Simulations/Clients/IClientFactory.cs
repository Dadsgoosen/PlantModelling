﻿using Grpc.Core;
using PlantSimulatorService.Simulations.Protos;

namespace PlantSimulatorService.Simulations.Clients
{
    public interface IClientFactory
    {
        public ISimulationClient CreateClient(string ip);
    }
}