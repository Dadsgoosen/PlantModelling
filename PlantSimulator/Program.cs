using System;
using System.Diagnostics;
using System.Numerics;
using System.Threading.Tasks;
using PlantSimulator.Helpers;
using PlantSimulator.Runtime;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;

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
