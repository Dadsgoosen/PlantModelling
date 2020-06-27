using System;
using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;
using PlantSimulator.Simulation.Operations.Carriers;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Plants.Fluids
{
    public class FluidsPlantCycle
    {
        private readonly Random random;

        private readonly IPlant plant;

        private readonly IPlantSimulatorOptionsService optionsService;

        private readonly ICarrierCollection<Sucrose> sucroseCarrierCollection;

        private readonly ILoggerAdapter<FluidsPlantCycle> logger;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public FluidsPlantCycle(IPlant plant, IPlantSimulatorOptionsService optionsService,
            ICarrierCollection<Sucrose> sucroseCarrierCollection, ILoggerAdapter<FluidsPlantCycle> logger)
        {
            this.plant = plant;
            this.optionsService = optionsService;
            this.sucroseCarrierCollection = sucroseCarrierCollection;
            this.logger = logger;
            random = new Random(optionsService.Options.Simulation.RandomSeed);
        }

        public void SpawnSucrose()
        {
            var top = FindTopInternode(out int leafCount);

            var bottomInternode = top;

            var topInternode = top;

            var bottomCells = bottomInternode.Cells.ToList();
            
            var topCells = topInternode.Cells.ToList();

            int amount = topCells.Count;

            float energy = Math.Min(10 * leafCount, 100);

            for (int i = 0; i < amount; i++)
            {
                logger.LogDebug("Add sucrose from @{From} to @{To}", bottomCells[i].Geometry.BottomCenter,
                    topCells[i].Geometry.BottomCenter);

                sucroseCarrierCollection.Add(bottomCells[i], topCells[i], bottomCells, 100);
            }
        }

        private IPlantPart FindTopInternode(out int leafCount)
        {
            int count = 0;

            IPlantPart upperInternode = null;

            Stack<IPlantPart> parts = new Stack<IPlantPart>(new[] {plant.ShootSystem.Stem.Internode});

            while (parts.Count > 0)
            {
                var part = parts.Pop();

                foreach (var conn in part.Connections)
                {
                    parts.Push(conn);
                }

                if (part.PartType == PlantPartType.Petiole)
                {
                    count++;
                }

                if (part.PartType == PlantPartType.Internode && !((Internode) part).HasUpperNode())
                {
                    upperInternode = part;
                }
            }

            leafCount = count > 0 ? count : 1;

            return upperInternode;
        }

        
    }
}