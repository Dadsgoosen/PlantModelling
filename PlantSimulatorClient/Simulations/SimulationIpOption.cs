using System;
using Microsoft.Extensions.Configuration;

namespace PlantSimulatorClient.Simulations
{
    internal class SimulationIpOption
    {
        public string Ip { get; }

        public SimulationIpOption(IConfiguration configuration)
        {
            Ip = FindIp(configuration);
        }

        private static string FindIp(IConfiguration configuration)
        {
            string kestrelIp = configuration["Kestrel:EndPoints:Http:Url"];
            string simulationIp = configuration["SimulationClient:Address"];

            if (!string.IsNullOrEmpty(simulationIp))
            {
                return simulationIp;
            }

            if (!string.IsNullOrEmpty(kestrelIp))
            {
                return kestrelIp;
            }

            throw new NullReferenceException("You must either provide 'SimulationClient:Address' or 'Kestrel:EndPoints:Http:Url' in the appsettings.json");
        }
    }
}