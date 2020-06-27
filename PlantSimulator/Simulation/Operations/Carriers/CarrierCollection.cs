using System.Collections.Generic;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;

namespace PlantSimulator.Simulation.Operations.Carriers
{
    public abstract class CarrierCollection<TFluid> : ICarrierCollection<TFluid> where TFluid : IFluid
    {
        private readonly IDictionary<IPlantCell, IFluidCarrier<TFluid>> carriers;

        protected CarrierCollection()
        {
            carriers = new Dictionary<IPlantCell, IFluidCarrier<TFluid>>();
        }

        public void Add(IPlantCell from, IPlantCell to, IEnumerable<IPlantCell> cells, float amount)
        {
            var carrier = CreateCarrier(from, to, cells, amount);
            
            Add(carrier);
        }

        public void Add(IFluidCarrier<TFluid> carrier)
        {
            carriers.Add(carrier.Destination, carrier);
        }

        public bool TryGet(IPlantCell cell, out IFluidCarrier<TFluid> carrier)
        {
            return carriers.TryGetValue(cell, out carrier);
        }

        public bool Has(IPlantCell cell)
        {
            return carriers.ContainsKey(cell);
        }

        public void Delete(IPlantCell cell)
        {
            if (carriers.ContainsKey(cell))
            {
                carriers.Remove(cell);
            }
        }

        public IFluidCarrier<TFluid> Get(IPlantCell cell)
        {
            if (carriers.TryGetValue(cell, out var fc))
            {
                return fc;
            }

            return null;
        }

        public IEnumerable<IFluidCarrier<TFluid>> GetInTransit()
        {
            return carriers.Values;
        }

        protected abstract IFluidCarrier<TFluid> CreateCarrier(IPlantCell from, IPlantCell to,
            IEnumerable<IPlantCell> cells, float amount);
    }
}