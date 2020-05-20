using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellGrower : ICellGrower
    {
        private readonly IPlant plant;

        private readonly SimulationEnvironment environment;

        private readonly ICellBodySystemSolver systemSolver;

        private static float PercentTowardsLight = 0.001F;

        public GenericCellGrower(IPlant plant, SimulationEnvironment environment, ICellBodySystemSolver systemSolver)
        {
            this.plant = plant;
            this.environment = environment;
            this.systemSolver = systemSolver;
        }

        public void GrowShootCell(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            MoveTopPointUpwards(cell.Geometry);
        }

        public void GrowRootCell(IPlantCell cell, IPlantPart part, SimulationStateSnapshot state)
        {
            MoveBottomPointDownwards(cell.Geometry);
        }

        private static void MoveTopPointUpwards(ICellGeometry geometry)
        {
            geometry.TopCenter.Y += 1;
        }

        private static void MoveBottomPointDownwards(ICellGeometry geometry)
        {
            geometry.BottomCenter.Y += 1;
        }

        private void MovePointTowardsLight(ICellGeometry geometry)
        {
            var lightSource = environment.LightPosition;

            var towards = new Vertex(lightSource.X * PercentTowardsLight, lightSource.Y * PercentTowardsLight, lightSource.Z * PercentTowardsLight);

            geometry.TopCenter.Add(towards);
        }
    }
}