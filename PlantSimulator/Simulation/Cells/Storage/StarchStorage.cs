namespace PlantSimulator.Simulation.Cells.Storage
{
    public class StarchStorage : Storage<Sucrose>
    {
        public StarchStorage(float amount) : base(amount)
        {
        }

        public StarchStorage(float amount, float capacity) : base(amount, capacity)
        {
        }

        protected override Sucrose CreateFluid(float amount)
        {
            return new Sucrose(amount);
        }
    }
}