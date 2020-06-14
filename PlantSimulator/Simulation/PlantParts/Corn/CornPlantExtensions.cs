using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using PlantSimulator.Simulation.PlantParts.Generic;
using PlantSimulator.Simulation.PlantParts.Helpers;

namespace PlantSimulator.Simulation.PlantParts.Corn
{
    public static class CornPlantExtensions
    {
        public static void AddCornPlant(this IServiceCollection service)
        {
            // Helper classes if they on't exist
            service.TryAddTransient<ICellCreatorHelper, CellCreatorHelper>();

            // Construct the plant with stem and main root
            service.AddTransient(provider => new GenericShootSystem(CreateStem(provider)));
            service.AddTransient(provider => new GenericRootSystem(CreateRoot(provider)));
            service.AddSingleton<IPlant, CornPlant>();
        }

        private static GenericStem CreateStem(IServiceProvider service)
        {
            var creator = service.GetService<ICellCreatorHelper>();

            return new GenericStem(creator.CreateCell(10), 0);
        }

        private static GenericRoot CreateRoot(IServiceProvider service)
        {
            var creator = service.GetService<ICellCreatorHelper>();

            return new GenericRoot(creator.CreateCell(5), new List<Root>());
        }
    }
}