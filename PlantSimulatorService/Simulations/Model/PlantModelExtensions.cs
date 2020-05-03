using System;
using System.Collections.Generic;
using System.Linq;
using PlantSimulatorService.Simulations.Protos;

namespace PlantSimulatorService.Simulations.Model
{
    internal static class PlantModelExtensions
    {
        public static SimulationState ToPlantModel(this Protos.SimulationState state)
        {
            return new SimulationState
            {
                Id = state.Id,
                SimulationTime = state.SimulationTime,
                Plant = state.Plant.ToPlantModel(),
                Date = DateTime.Now
            };
        }

        private static PlantModel ToPlantModel(this PlantModelState state)
        {
            return new PlantModel
            {
                RootSystem = state.RootSystem.ToPlantNodeModels(),
                ShootSystem = state.ShootSystem.ToPlantNodeModels()
            };
        }

        private static IEnumerable<PlantNodeModel> ToPlantNodeModels(this IEnumerable<PlantNodeModelState> states)
        {
            var nodeStates = states.ToArray();

            PlantNodeModel[] models = new PlantNodeModel[nodeStates.Length];

            for (int i = 0; i < nodeStates.Length; i++)
            {
                var nodeState = nodeStates[i];

                models[i] = new PlantNodeModel
                {
                    Thickness = nodeState.Thickness,
                    X = nodeState.X,
                    Y = nodeState.Y,
                    Connections = nodeState.Connections.ToPlantNodeModels()
                };
            }

            return models;
        }
    }
}