using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;
using PlantSimulator.Simulation.Operations.Carriers;
using PlantSimulator.Simulation.Operations.Transporters;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;
using PlantSimulator.Simulation.Plants.Corn;

namespace PlantSimulator.Simulation.Plants.Fluids
{
    public static class FluidsPlantExtension
    {
        public static void AddFluidsPlant(this IServiceCollection service)
        {
            // Helper classes if they don't exist
            service.TryAddTransient<IPlantPartCellCreator, PlantPartCellCreator>();
            service.AddSingleton<FluidTransporter<Sucrose>, SucroseTransporter>();
            service.AddSingleton<ICarrierCollection<Sucrose>, SucroseCarrierCollection>();
            service.AddSingleton<FluidsPlantCycle>();

            // Part Factories
            service.AddTransient(p => CornCellTypeLocator.GetCornCellTypeLocator());

            // Construct the plant with stem and main root
            service.AddTransient(provider => new GenericShootSystem(CreateStem(provider)));
            service.AddTransient(provider => new GenericRootSystem(CreateRoot(provider)));
            service.AddSingleton<IPlant, FluidsPlant>();
        }

        private static GenericStem CreateStem(IServiceProvider service)
        {
            var creator = service.GetService<IPlantPartCellCreator>();

            var cells = new List<IPlantCell>();

            cells.AddRange(creator.CreateCells(21, 17, new Vector3(0), 0f));

            Internode internode = new GenericInternode(cells, 0);

            return new GenericStem(internode, 0);
        }

        private static GenericRoot CreateRoot(IServiceProvider service)
        {
            var creator = service.GetService<IPlantPartCellCreator>();

            var cells = new List<IPlantCell>();

            cells.AddRange(creator.CreateCells(11, 9, new Vector3(0, -10, 0), 10f));

            return new GenericRoot(cells, 0);
        }
    }
}