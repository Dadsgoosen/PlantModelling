using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Outputs.Protos;
using SimulationState = PlantSimulator.Outputs.Models.SimulationState;
using GrpcSimulationState = PlantSimulator.Outputs.Protos.SimulationState;

namespace PlantSimulator.Outputs
{
    internal static class GrpcStateTransmitterExtensions
    {
        public static GrpcSimulationState ToGrpcSimulationState(this SimulationState state)
        {
            return MapSimulationState(state);
        }


        private static GrpcSimulationState MapSimulationState(SimulationState state)
        {
            PlantModelState plantModelState = new PlantModelState
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

        private static IEnumerable<PlantNodeModelState> MapNodeSystem(IEnumerable<Models.PlantNodeModelState> nodes)
        {
            Models.PlantNodeModelState[] plantNodeModelStates = nodes as Models.PlantNodeModelState[] ?? nodes.ToArray();

            IList<PlantNodeModelState> grpcNodes = new List<PlantNodeModelState>(plantNodeModelStates.Length);

            foreach (var node in plantNodeModelStates)
            {
                grpcNodes.Add(MapNodeState(node));
            }

            return grpcNodes;
        }

        private static PlantNodeModelState MapNodeState(Models.PlantNodeModelState node)
        {
            return new PlantNodeModelState
            {
                X = 0,
                Y = 0,
                Thickness = node.Thickness,
                Connections = { MapNodeSystem(node.Connections) }
            };
        }
    }
}