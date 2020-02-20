using System;
using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulator.Simulation
{
    public interface ISimulator : IDisposable
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync();
    }
}