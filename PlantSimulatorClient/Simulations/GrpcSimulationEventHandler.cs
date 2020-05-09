using System;
using PlantSimulator.Outputs;
using PlantSimulator.Runtime;
using PlantSimulator.Simulation;
using PlantSimulatorClient.Simulations.Protos;

namespace PlantSimulatorClient.Simulations
{
    public class GrpcSimulationEventHandler : ISimulatorEventHandler
    {
        private readonly SimulationClientService.SimulationClientServiceClient client;

        private readonly ISimulationStateFactory stateFactory;

        public GrpcSimulationEventHandler(SimulationClientService.SimulationClientServiceClient client, ISimulationStateFactory stateFactory)
        {
            this.client = client;
            this.stateFactory = stateFactory;
        }

        public void OnSimulationTick(object sender, PlantSimulatorTickEvent tickEvent)
        {
            var state = stateFactory.Create(tickEvent.Id, tickEvent.Plant, new SimulationStateSnapshot(tickEvent.TickTimer));

            var grpc = state.ToGrpcSimulationState();

            client.TransmitState(grpc);
        }
    }
}