using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Operations.Carriers
{
    public class FluidCarrier<TFluid> : IFluidCarrier<TFluid> where TFluid : IFluid
    {
        public IPlantCell Destination { get; }

        public IPlantCell Current { get; set; }

        public IPlantCell ClosestTransportCell { get; }

        private readonly PlantCellType transportType;

        public bool IsInTransportCell => ClosestTransportCell.CellType == transportType;

        public TFluid Fluid { get; }

        public FluidCarrier(IPlantCell destination, IPlantCell current, IPlantCell closestTransportCell, PlantCellType transportType, TFluid fluid)
        {
            Destination = destination;
            Current = current;
            ClosestTransportCell = closestTransportCell;
            this.transportType = transportType;
            Fluid = fluid;
        }
    }
}