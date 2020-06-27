namespace PlantSimulator.Simulation.Cells.Storage
{
    public abstract class Storage<T> : IStorage<T> where T : IFluid
    {
        private float capacity;

        public float Amount { get; set; }

        protected Storage(float amount)
        {
            Amount = amount;
        }

        protected Storage(float amount, float capacity)
        {
            Amount = amount;
            this.capacity = capacity;
        }

        public T Get(float amount)
        {
            if (Amount < amount)
            {
                return GetFluid(Amount);
            }

            return GetFluid(amount);
        }

        private T GetFluid(float amount)
        {
            Amount -= amount;

            return CreateFluid(amount);
        }

        protected abstract T CreateFluid(float amount);

        public void Store(T store)
        {
            Amount += store.Amount;
        }
    }
}