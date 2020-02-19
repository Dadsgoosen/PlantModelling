using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulator.Runtime
{
    public interface IAsyncRuntime
    {
        void OnStartedAsync();

        void OnStoppingAsync();

        void OnStoppedAsync();
    }
}