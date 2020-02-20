using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulator.Runtime
{
    public interface IRuntime
    {
        Task StartAsync(CancellationToken cancellationToken);
        Task StopAsync(CancellationToken cancellationToken);
    }
}