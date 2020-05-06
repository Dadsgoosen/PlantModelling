using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Google.Protobuf.Collections;
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
                    Coordinates = ConvertToVectors(nodeState.Coordinates),
                    Connections = nodeState.Connections.ToPlantNodeModels()
                };
            }

            return models;
        }

        private static Vector2[] ConvertToVectors(IReadOnlyCollection<Coordinate> coordinates)
        {
            var vectors = new Vector2[coordinates.Count];
            
            int i = 0;
            
            foreach (var vector in vectors)
            {
                vectors[i] = new Vector2(vector.X, vector.Y);
                i++;
            }

            return vectors;
        }
    }
}