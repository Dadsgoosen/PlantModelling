using System;
using System.Threading;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Runtime;
using Serilog;

namespace PlantSimulator
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Main starting");
            try
            {
                new PlantSimulatorHostBuilder(args).Build().Run();
            }
            catch (Exception e)
            {
                Log.Logger.Fatal(e, e.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
