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
            Console.WriteLine("Main starting");

            string path = "F:\\Speciale\\Data\\memory_usage.csv";

            int count = 0;

            IPlant plant;
            
            for (int i = 0; i < 250; i++)
            {
                count += 1000;

                plant = CreatePlant(count);

                Statistics.SaveMemoryUsage(path, plant);

                GC.Collect();
            }

            //await new PlantSimulatorHostBuilder(args).Run();
        }

        private static IPlant CreatePlant(int cellCount)
        {
            IRootSystem root = new GenericRootSystem(new GenericRoot(new IPlantCell[0], new Root[0]));

            IShootSystem shoot = new GenericShootSystem(new GenericStem(CreateCells(cellCount), 0));

            return new GenericPlant(shoot, root);
        }

        private static IPlantCell[] CreateCells(int count)
        {
            IPlantCell[] cells = new IPlantCell[count];

            for (int i = 0; i < count; i++)
            {
                var geo = new CellGeometry(new Vector3(0), Vector3.One, new Face(new[] {Vector2.One, Vector2.One, Vector2.One, Vector2.One, Vector2.One, Vector2.One}));
                cells[i] = new XylemCell(geo, new Vacuole(), new CellWall());
            }

            return cells;
        }
    }
}
