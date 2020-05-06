using System.Threading.Tasks;
using PlantSimulator.Outputs;
using PlantSimulatorClient.Simulations.Protos;
using SimulationState = PlantSimulator.Outputs.Models.SimulationState;

namespace PlantSimulatorClient.Simulations
{
    public class GrpcStateTransmitter : IStateTransmitter
    {
        private readonly SimulationClientService.SimulationClientServiceClient client;

        public GrpcStateTransmitter(SimulationClientService.SimulationClientServiceClient client)
        {
            this.client = client;
        }

        public async Task TransmitStateAsync(SimulationState state)
        {
            await client.TransmitStateAsync(state.ToGrpcSimulationState());
        }
    }
}