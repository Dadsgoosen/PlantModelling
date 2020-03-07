using System;
using System.Collections.Generic;
using PlantSimulator.Simulation.Geometry;

namespace PlantSimulator.Simulation.Cells
{
    public interface IPlantCell
    {
        public ICellGeometry Geometry { get; }

        public ICellWall CellWall { get; }

        public IVacuole Vacuole { get; }

        public float TurgorPressure { get; }
    }
}