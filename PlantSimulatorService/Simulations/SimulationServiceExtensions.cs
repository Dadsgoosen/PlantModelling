﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PlantSimulatorService.Context;
using PlantSimulatorService.Simulations.Api;
using PlantSimulatorService.Simulations.Clients;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Simulations
{
    internal static class SimulationServiceExtensions
    {
        public static IServiceCollection AddSimulation(this IServiceCollection service, IConfiguration configuration)
        {
            service.Configure<FileStorageOptions>(configuration.GetSection("Simulation:Storage"));

            service.AddSingleton<IClientContext, ClientContext>();
            service.AddSingleton<ISimulationContext, SimulationContext>();
            service.AddSingleton<IFileHandler<SimulationState>, JsonFileHandler<SimulationState>>();
            service.AddSingleton<ISimulationStorage, SimulationFileStorage>();
            service.AddSingleton<IClientFactory, SimulationClientFactory>();
            service.AddSingleton<IClientCollection, SimulationClientCollection>();
            service.AddSingleton<ISimulationApiInformation, SimulationApiInformation>();
            service.AddSingleton<IClientHandler, GrpcSimulationClientHandler>();
            service.AddSingleton<ISimulationClientContext, SimulationClientContext>();
            return service;
        }
    }
}