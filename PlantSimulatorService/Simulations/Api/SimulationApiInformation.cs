using Newtonsoft.Json;
using PlantSimulatorService.Simulations.Clients;

namespace PlantSimulatorService.Simulations.Api
{
    public class SimulationApiInformation : ISimulationApiInformation
    {
        [JsonIgnore]
        private readonly IClientCollection clientCollection;

        public SimulationApiInformation(IClientCollection clientCollection)
        {
            this.clientCollection = clientCollection;
        }

        public string Version => "0.01";

        public int ConnectedClients => clientCollection.Count();
    }
}