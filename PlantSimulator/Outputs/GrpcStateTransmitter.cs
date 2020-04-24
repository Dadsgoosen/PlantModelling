using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using Grpc.Net.Client;
using PlantSimulator.Outputs.Models;
using PlantSimulator.Outputs.Protos;
using SimulationState = PlantSimulator.Outputs.Models.SimulationState;
using GrpcSimulationState = PlantSimulator.Outputs.Protos.SimulationState;
using GrpcPlantModelState = PlantSimulator.Outputs.Protos.PlantModelState;
using GrpcPlantNodeModelState = PlantSimulator.Outputs.Protos.PlantNodeModelState;
using PlantModelState = PlantSimulator.Outputs.Models.PlantModelState;
using PlantNodeModelState = PlantSimulator.Outputs.Models.PlantNodeModelState;

namespace PlantSimulator.Outputs
{
    public class GrpcStateTransmitter : IStateTransmitter
    {
        private readonly SimulationStateService.SimulationStateServiceClient client;

        public GrpcStateTransmitter(GrpcChannel channel)
        {
            client = new SimulationStateService.SimulationStateServiceClient(channel);
        }

        public async Task TransmitStateAsync(SimulationState state)
        {
            await client.TransmitStateAsync(MapSimulationState(state));
        }

        private static GrpcSimulationState MapSimulationState(SimulationState state)
        {
            GrpcPlantModelState plantModelState = new GrpcPlantModelState
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
            IList<GrpcPlantNodeModelState> grpcNodes = new List<GrpcPlantNodeModelState>(nodes.Count());

            foreach (var node in nodes)
            {
                grpcNodes.Add(MapNodeState(node));
            }

            return grpcNodes;
        }

        private static GrpcPlantNodeModelState MapNodeState(PlantNodeModelState node)
        {
            return new GrpcPlantNodeModelState
            {
                X = node.X,
                Y = node.Y,
                Thickness = node.Thickness
            };
        }
    }
}