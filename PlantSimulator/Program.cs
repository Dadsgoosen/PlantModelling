using System.Threading.Tasks;
using PlantSimulator.Runtime;

namespace PlantSimulator
{
    internal sealed class Program
    {
        private static async Task Main(string[] args)
        {
            await new PlantSimulatorHostBuilder(args).Run();
        }

    }
}
