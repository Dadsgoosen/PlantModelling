using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Operations.Carriers
{
    public interface ICarrierCollection<TFluid> where TFluid : IFluid
    {
        public void Add(IPlantCell from, IPlantCell to, IEnumerable<IPlantCell> cells, float amount);
        public void Add(IFluidCarrier<TFluid> carrier);
        public bool TryGet(IPlantCell cell, out IFluidCarrier<TFluid> carrier);
        public bool Has(IPlantCell cell);
        public void Delete(IPlantCell cell);
        public IFluidCarrier<TFluid> Get(IPlantCell cell);

        public IEnumerable<IFluidCarrier<TFluid>> GetInTransit();
    }
}