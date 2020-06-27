using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class FluidsGenericCellGrower : GenericCellGrower
    {
        private const float Capacity = 100;

        public FluidsGenericCellGrower(IPlant plant, SimulationEnvironment environment,
            ICellBodySystemSolver systemSolver, IPlantSimulatorOptionsService optionsService) : base(plant, environment,
            systemSolver, optionsService)
        {
        }

        protected override Vector3 DetermineGrowthFactor(IPlantCell cell, SimulationStateSnapshot snapshot)
        {
            float range = Options.Plant.GrowthRange.RandomNumberBetween();

            float energy = cell.StarchStorage.Amount;

            cell.StarchStorage.Amount = 0;

            float y = range * energy;

            return new Vector3(y, y, 1);
        }

    }
}