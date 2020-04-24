using System.Collections.Generic;

namespace PlantSimulatorService.Simulations.Clients
{
    public interface IClientCollection : IEnumerable<ISimulationClient>
    {
        ISimulationClient GetClient(string id);

        IEnumerable<ISimulationClient> GetClients();

        void AddClient(ISimulationClient client);

        void DeleteClient(string id);

        int Count();
    }
}