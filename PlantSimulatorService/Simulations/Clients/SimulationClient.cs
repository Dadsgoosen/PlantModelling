using System.Numerics;
using System.Threading.Tasks;
using Grpc.Core;
using PlantSimulatorService.Simulations.Model.Options;
using PlantSimulatorService.Simulations.Protos;
using SimulationEnvironmentOptions = PlantSimulatorService.Simulations.Protos.SimulationEnvironmentOptions;
using SimulationOptions = PlantSimulatorService.Simulations.Protos.SimulationOptions;
using SimulationPlantOptions = PlantSimulatorService.Simulations.Protos.SimulationPlantOptions;

namespace PlantSimulatorService.Simulations.Clients
{
    public class SimulationClient : ISimulationClient
    {
        public string Id { get; set; }

        public bool Available { get; private set; }

        public string Host { get; set; }

        public SimulationServerService.SimulationServerServiceClient Client { get; set; }

        public SimulationClient()
        {
            Available = true;
        }

        public async Task StartAsync(PlantSimulationOptions options)
        {
            try
            {
                await Client.StartSimulationAsync(ToGrpcOptions(options));
                Available = false;
            }
            catch (RpcException)
            {
                Available = true;
            }
        }

        public async Task StopAsync()
        {
            Available = true;
            await Client.StopSimulationAsync(new StopSimulationRequest());
        }

        private static SimulationConfiguration ToGrpcOptions(PlantSimulationOptions options)
        {
            return new SimulationConfiguration
            {
                Id = options.Id,
                Simulation = new SimulationOptions
                {
                    TickTime = options.Simulation.TickTime,
                    TickEventTime = options.Simulation.TickEventTime,
                    RandomSeed = options.Simulation.RandomSeed
                },
                Environment = new SimulationEnvironmentOptions
                {
                    LightSource = VectorToCoordinate(options.Environment.LightSource),
                    Temperature = options.Environment.Temperature
                },
                Plant = new SimulationPlantOptions
                {
                    Branches = RangeToIntRange(options.Plant.Branches),
                    SubBranches = RangeToIntRange(options.Plant.SubBranches),
                    InternodeLength = RangeToIntRange(options.Plant.InternodeLength),
                    PetioleLength = RangeToIntRange(options.Plant.PetioleLength),
                    Axil = options.Plant.Axil,
                }
            };
        }

        private static Coordinate VectorToCoordinate(Vector2 vector)
        {
            return new Coordinate {X = vector.X, Y = vector.Y};
        }

        private static IntRange RangeToIntRange(Range<int> range)
        {
            return new IntRange {Min = range.Min, Max = range.Max};
        }
    }
}