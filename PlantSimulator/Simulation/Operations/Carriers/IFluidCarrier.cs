using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Operations.Carriers
{
    public interface IFluidCarrier<out TFluid> where TFluid : IFluid
    {
        public IPlantCell Destination { get; }

        public IPlantCell Current { get; set; }

        public IPlantCell ClosestTransportCell { get; }

        public bool IsInTransportCell { get; }

        public TFluid Fluid { get; }
    }
}