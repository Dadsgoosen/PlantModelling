using System;
using System.Linq;
using System.Threading.Tasks;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations
{
    public class GenericCellBodySystemSolver : ICellBodySystemSolver
    {
        private readonly ICellCollisionDetection collisionDetection;

        private readonly ICellSizer cellSizer;

        public GenericCellBodySystemSolver(ICellCollisionDetection collisionDetection, ICellSizer cellSizer)
        {
            this.collisionDetection = collisionDetection;
            this.cellSizer = cellSizer;
        }

        public void Solve(IPlantPart part)
        {
            var cells = part.Cells.ToArray();

            do
            {
                DoubleIterateCells(cells, (a, b) =>
                {
                    if (a.Equals(b) || !collisionDetection.Colliding(a, b, false))
                    {
                        return;
                    }

                    SolveCell(a, b);
                });
            } while (DoesAnyCollide(cells));
        }

        private void SolveCell(IPlantCell a, IPlantCell b)
        {
            cellSizer.ResizeHeight(a, b);

            // cellSizer.ResizeWidth(a, b);
        }

        private bool DoesAnyCollide(IPlantCell[] cells)
        {
            var collides = false;

            DoubleIterateCells(cells, (a, b) =>
            {
                if (a.Equals(b)) return;

                if (collisionDetection.Colliding(a, b, true))
                {
                    collides = true;
                }
            });

            return collides;
        }

        private void DoubleIterateCells(IPlantCell[] cells, Action<IPlantCell, IPlantCell> action)
        {
            Parallel.For(0, cells.Length, i =>
            {
                Parallel.For(0, cells.Length,
                    j => {
                        if (i == j) return;
                        action(cells[i], cells[j]);
                    });
            });

        }
    }
}