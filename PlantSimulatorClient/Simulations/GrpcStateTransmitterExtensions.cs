using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using PlantSimulatorClient.Simulations.Protos;
using GrpcSimulationState = PlantSimulatorClient.Simulations.Protos.SimulationState;
using GrpcPlantModelState = PlantSimulatorClient.Simulations.Protos.PlantModelState;
using GrpcPlantNodeModelState = PlantSimulatorClient.Simulations.Protos.PlantNodeModelState;
using PlantModelState = PlantSimulator.Outputs.Models.PlantModelState;
using PlantNodeModelState = PlantSimulator.Outputs.Models.PlantNodeModelState;
using SimulationState = PlantSimulator.Outputs.Models.SimulationState;

namespace PlantSimulatorClient.Simulations
{
    internal static class GrpcStateTransmitterExtensions
    {
        public static GrpcSimulationState ToGrpcSimulationState(this SimulationState state)
        {
            return MapSimulationState(state);
        }


        private static GrpcSimulationState MapSimulationState(SimulationState state)
        {
            var plantModelState = new GrpcPlantModelState
            {
                RootSystem = { MapNodeSystem(state.Plant.RootSystem) },
                ShootSystem = { MapNodeSystem(state.Plant.ShootSystem) }
            };

            return new GrpcSimulationState
            {
                Id = state.Id,
                SimulationTime = state.SimulationTime,
                Plant = plantModelState
            };
        }

        private static IEnumerable<GrpcPlantNodeModelState> MapNodeSystem(IEnumerable<PlantNodeModelState> nodes)
        {
            PlantNodeModelState[] plantNodeModelStates = nodes as PlantNodeModelState[] ?? nodes.ToArray();

            IList<GrpcPlantNodeModelState> grpcNodes = new List<GrpcPlantNodeModelState>(plantNodeModelStates.Length);

            foreach (var node in plantNodeModelStates)
            {
                grpcNodes.Add(MapNodeState(node));
            }

            return grpcNodes;
        }

        private static GrpcPlantNodeModelState MapNodeState(PlantNodeModelState node)
        {
            return new GrpcPlantNodeModelState
            {
                Coordinates = { ConvertVectorToCoordinates(node.Coordinates) },
                Thickness = node.Thickness,
                Connections = { MapNodeSystem(node.Connections) }
            };
        }

        private static IEnumerable<Coordinate> ConvertVectorToCoordinates(Vector2[] vectors)
        {
            IList<Coordinate> coordinates = new List<Coordinate>(vectors.Length);

            for (int i = 0; i < vectors.Length; i++)
            {
                coordinates.Add(new Coordinate {X = vectors[i].X, Y = vectors[i].Y});
            }

            return coordinates;
        }
    }
}