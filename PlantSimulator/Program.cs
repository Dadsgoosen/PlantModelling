using System;
using System.Threading.Tasks;
using PlantSimulator.Runtime;

namespace PlantSimulator
{
    internal sealed class Program
    {
        private static async Task Main(string[] args)
        {
            Console.WriteLine("Main starting");

            await new PlantSimulatorHostBuilder(args).Run();
        }
    }
}
