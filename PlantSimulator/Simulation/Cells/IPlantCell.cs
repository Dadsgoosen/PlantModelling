using System;
using System.Collections.Generic;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public interface IPlantCell
    {
        public object Synchronizer { get; }

        public PlantCellType CellType { get; }

        public ICellGeometry Geometry { get; }

        public IPlantCell[] Neighbors { get; }

        public ICellWall CellWall { get; }

        public IVacuole Vacuole { get; }

        public float Turgidity { get; }

        public bool IsDead { get; }

        public void Kill();
    }
}