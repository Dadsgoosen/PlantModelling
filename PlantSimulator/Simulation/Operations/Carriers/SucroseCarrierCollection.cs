using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Operations.Carriers
{
    public class SucroseCarrierCollection : CarrierCollection<Sucrose>
    {
        private readonly ILoggerAdapter<SucroseCarrierCollection> logger;

        public SucroseCarrierCollection(ILoggerAdapter<SucroseCarrierCollection> logger)
        {
            this.logger = logger;
        }

        protected override IFluidCarrier<Sucrose> CreateCarrier(IPlantCell from, IPlantCell to, IEnumerable<IPlantCell> cells, float amount)
        {
            var closest = ClosestTransporterCell(PlantCellType.Phloem, from, cells);

            return new FluidCarrier<Sucrose>(from, to, closest, PlantCellType.Phloem, new Sucrose(amount));
        }

        private IPlantCell ClosestTransporterCell(PlantCellType type, IPlantCell current, IEnumerable<IPlantCell> cells)
        {
            float closestDistance = float.MaxValue;

            IPlantCell closestCell = null;

            foreach (var cell in cells)
            {
                if (cell.CellType != type) continue;

                float distance = Vector3.Distance(cell.Geometry.TopCenter, current.Geometry.TopCenter);

                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestCell = cell;
                }
            }

            if (closestCell == null)
            {
                logger.LogFatal("Could not find closest cell of type {CellType} for cell at {Cell}", type, current.Geometry.TopCenter);
            }

            return closestCell;
        }
    }
}