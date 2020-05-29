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

        public GenericCellBodySystemSolver(ICellCollisionDetection collisionDetection)
        {
            this.collisionDetection = collisionDetection;
        }

        public void Solve(IPlantPart part)
        {
            var cellArray = part.Cells.ToArray();

            for (int i = 0; i < cellArray.Length; i++)
            {
                SolveCell(cellArray[i], cellArray);
            }
        }

        private void SolveCell(IPlantCell cell, IPlantCell[] cells)
        {
            
        }

        private void ResizeCell(IPlantCell main, IPlantCell colliding)
        {
            Vector2[] mainFace = main.Geometry.Face.Points;

            Vector2[] collidingFace = colliding.Geometry.Face.Points;

            for (int i = 0; i < mainFace.Length; i++)
            {
                // if (!collisionDetection.(mainFace[i], collidingFace)) continue;
                

            }
        }


    }

}