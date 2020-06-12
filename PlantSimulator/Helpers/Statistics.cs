using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Helpers
{
    internal static class Statistics
    {
        private const int Mb = 1024 * 1024;

        public static long MemoryUsage()
        {
            using (Process process = Process.GetCurrentProcess())
            {
                return process.PrivateMemorySize64 / Mb;
            }
        }

        public static void SaveMemoryUsage(string path, IPlant plant)
        {
            StringBuilder str = new StringBuilder();

            if (!File.Exists(path))
            {
                str.AppendLine("\"Part Count\";\"Cell Count\";\"Usage (MB)\"");
            }

            int cellCount = 0;

            int partCount = 0;

            var parts = new Stack<IPlantPart>(new IPlantPart[] { plant.ShootSystem.Stem, plant.RootSystem.PrimaryRoot });

            while (parts.Count > 0)
            {
                var p = parts.Pop();

                cellCount += p.Cells.Count();

                partCount++;

                foreach (var c in p.Connections)
                {
                    parts.Push(c);
                }
            }

            str.AppendLine($"\"{partCount}\";\"{cellCount}\";\"{MemoryUsage()}\"");

            File.AppendAllText(path, str.ToString(), Encoding.UTF8);
        }
    }
}