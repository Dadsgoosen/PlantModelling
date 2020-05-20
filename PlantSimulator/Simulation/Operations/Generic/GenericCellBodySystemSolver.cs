using System.Linq;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellBodySystemSolver : ICellBodySystemSolver
    {
        private readonly ICollisionDetection<IPlantCell> collisionDetection;

        public GenericCellBodySystemSolver(ICollisionDetection<IPlantCell> collisionDetection)
        {
            this.collisionDetection = collisionDetection;
        }

        public void Solve(IPlantCell cell, IPlantPart part)
        {
            foreach (var partCell in part.Cells)
            {
                if (collisionDetection.IsColliding(cell, partCell))
                {
                    
                }
            }
        }

        private void ResizeCell(IPlantCell main, IPlantCell colliding)
        {

        }
    }
}