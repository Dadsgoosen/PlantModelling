using System.Numerics;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Options;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellGrower : ICellGrower
    {
        private readonly IPlant plant;

        private readonly SimulationEnvironment environment;

        private readonly ICellBodySystemSolver systemSolver;

        private readonly IPlantSimulatorOptionsService optionsService;

        private IPlantSimulatorOptions Options => optionsService.Options;

        public GenericCellGrower(IPlant plant, SimulationEnvironment environment, ICellBodySystemSolver systemSolver, IPlantSimulatorOptionsService optionsService)
        {
            this.plant = plant;
            this.environment = environment;
            this.systemSolver = systemSolver;
            this.optionsService = optionsService;
        }

        public void GrowShootCell(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            MoveTopPointUpwards(cell, part, state);
        }

        public void GrowRootCell(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            MoveBottomPointDownwards(cell, part, state);
        }

        private void MoveTopPointUpwards(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            var geo = cell.Geometry;

            geo.TopCenter += DetermineGrowth(cell, part, state);
        }

        private void MoveBottomPointDownwards(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            var geo = cell.Geometry;

            geo.BottomCenter -= DetermineGrowth(cell, part, state);
        }
        
        private Vector3 DetermineGrowth(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            var growth = DetermineGrowthDirection(cell, part) * DetermineGrowthFactor(state);

            return growth;
        }

        private Vector3 DetermineGrowthDirection(IPlantCell cell, IPlantPart part)
        {
            var geo = cell.Geometry;

            var direction = new Vector3(0, 1, 0);

            var outwards = Vector3.Zero;

            if (part.BranchCount > 0)
            {
                if (geo.BottomCenter.X > 0)
                {
                    outwards += new Vector3(2, 0, 0);
                }
                else if (geo.BottomCenter.X < 0)
                {
                    outwards += new Vector3(-2, 0, 0);
                }
            }

            return direction + outwards;
        }

        private Vector3 DetermineGrowthFactor(SimulationStateSnapshot snapshot)
        {
            float y = Options.Plant.GrowthRange.RandomNumberBetween();

            y += snapshot.CurrentTime / 25f * 0.0013f;

            return new Vector3(y, y, 1);
        }
    }
}