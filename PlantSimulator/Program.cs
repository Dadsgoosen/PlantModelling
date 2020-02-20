using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Runtime;
using Serilog;

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
