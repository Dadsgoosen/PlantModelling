using System;

namespace PlantSimulatorService.Simulations.Clients
{
    public class NoAvailableClientsException : Exception
    {
        public NoAvailableClientsException(string message) : base(message) { }
    }
}