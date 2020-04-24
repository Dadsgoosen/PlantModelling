using System.Collections;
using System.Collections.Generic;

namespace PlantSimulatorService.Simulations.Clients
{
    public class SimulationClientCollection : IClientCollection
    {
        private readonly IDictionary<string, ISimulationClient> clients;

        public SimulationClientCollection()
        {
            clients = new Dictionary<string, ISimulationClient>();
        }

        public ISimulationClient GetClient(string id)
        {
            return clients.TryGetValue(id, out var client) ? client : null;
        }

        public IEnumerable<ISimulationClient> GetClients()
        {
            return clients.Values;
        }

        public void AddClient(ISimulationClient client)
        {
            clients.Add(client.Id, client);
        }

        public void DeleteClient(string id)
        {
            if (!clients.ContainsKey(id))
            {
                return;
            }

            clients.Remove(id);
        }

        public int Count()
        {
            return clients.Count;
        }

        public IEnumerator<ISimulationClient> GetEnumerator()
        {
            return clients.Values.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}