namespace PlantSimulator.Simulation.Cells.Storage
{
    public class WaterStorage : Storage<Water>
    {
        public WaterStorage(float amount) : base(amount)
        {
        }

        public WaterStorage(float amount, float capacity) : base(amount, capacity)
        {
        }

        protected override Water CreateFluid(float amount)
        {
            return new Water(amount);
        }
    }
}