using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Transporters
{
    public interface ITransporter
    {
        public void Transport(IPlantPart part);
    }
}