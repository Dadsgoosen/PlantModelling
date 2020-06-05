using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
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
                for (int i = 0; i < cells.Length; i++)
                {
                    for (int j = 0; j < cells.Length; j++)
                    {
                        if (cells[i].Equals(cells[j]) || !collisionDetection.Colliding(cells[i], cells[j], false))
                        {
                            continue;
                        }

                        SolveCell(cells[i], cells[j]);
                    }
                }
            } while (DoesAnyCollide(cells));
        }

        private void SolveCell(IPlantCell a, IPlantCell b)
        {
            cellSizer.ResizeHeight(a, b);

            cellSizer.ResizeWidth(a, b);
        }

        private bool DoesAnyCollide(IPlantCell[] cells)
        {
            for (int i = 0; i < cells.Length; i++)
            {
                for (int j = 0; j < cells.Length; j++)
                {
                    if (cells[i].Equals(cells[j])) continue;
                    if (collisionDetection.Colliding(cells[i], cells[j], false)) return true;
                }
            }

            return false;
        }
    }

}