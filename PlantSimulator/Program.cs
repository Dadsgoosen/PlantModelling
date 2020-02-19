using System;
using Microsoft.Extensions.Hosting;
using PlantSimulator.Runtime;
using Serilog;

namespace PlantSimulator
{
    internal sealed class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Main starting");

            try
            {
                PlantSimulatorHostBuilder
                    .Build(args)
                    .RunConsoleAsync();
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e.Message);
                Log.Logger.Fatal(e, e.Message);
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }
    }
}
