namespace PlantSimulatorService.Simulations.Api
{
    public interface ISimulationApiInformation
    {
        public string Version { get; }

        public int ConnectedClients { get; }
    }
}