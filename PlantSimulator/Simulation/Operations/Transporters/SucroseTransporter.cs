using System.Collections.Generic;
using System.Linq;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations.Carriers;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Transporters
{
    public class SucroseTransporter : FluidTransporter<Sucrose>
    {
        public SucroseTransporter(ICellCollisionDetection collisionDetection, IGeometryHelper geometryHelper,
            ICarrierCollection<Sucrose> sucroseCarrierCollection, ILoggerAdapter<FluidTransporter<Sucrose>> logger) : base(collisionDetection, geometryHelper, sucroseCarrierCollection, logger)
        {
        }

        public override void Transport(IPlantPart part)
        {
            if (!part.Cells.Any()) return;

            var inTransit = CarrierCollection.GetInTransit();

            foreach (var transit in inTransit)
            {
                if (MoveCarrier(transit, part))
                {
                    Logger.LogDebug("{Transit} reached goal", transit.Destination.Geometry.TopCenter);

                    transit.Current.StarchStorage.Amount += transit.Fluid.Amount;

                    CarrierCollection.Delete(transit.Destination);
                }
            }
        }
    }
}