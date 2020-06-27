namespace PlantSimulator.Simulation.Cells.Storage
{
    public interface IStorage<T> where T : IFluid
    {
        public float Amount { get; }
        public T Get(float amount);
        public void Store(T store);
    }
}