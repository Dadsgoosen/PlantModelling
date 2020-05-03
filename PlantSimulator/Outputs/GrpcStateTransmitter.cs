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
            await client.TransmitStateAsync(state.ToGrpcSimulationState());
        }
    }
}