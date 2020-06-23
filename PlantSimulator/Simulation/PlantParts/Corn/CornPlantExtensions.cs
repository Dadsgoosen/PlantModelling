using System;
using System.Collections.Generic;
using System.Numerics;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlantSimulator.Simulation.PlantParts.Factories;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Corn
{
    public static class CornPlantExtensions
    {
        public static void AddCornPlant(this IServiceCollection service)
        {
            // Helper classes if they don't exist
            service.TryAddTransient<IPlantPartCellCreator, PlantPartCellCreator>();

            // Part Factories

            service.AddTransient(p => CornCellTypeLocator.GetCornCellTypeLocator());

            // Construct the plant with stem and main root
            service.AddTransient(provider => new GenericShootSystem(CreateStem(provider)));
            service.AddTransient(provider => new GenericRootSystem(CreateRoot(provider)));
            service.AddSingleton<IPlant, CornPlant>();
        }

        private static GenericStem CreateStem(IServiceProvider service)
        {
            var creator = service.GetService<IPlantPartCellCreator>();

            var internodeCells = creator.CreateCells(21, 17, new Vector3(0), 0f);

            Internode internode = new GenericInternode(internodeCells, 0);

            return new GenericStem(internode, 0);
        }

        private static GenericRoot CreateRoot(IServiceProvider service)
        {
            var creator = service.GetService<IPlantPartCellCreator>();

            var cells = creator.CreateCells(11, 9, new Vector3(0), 0f);

            return new GenericRoot(cells, 0);
        }
    }
}