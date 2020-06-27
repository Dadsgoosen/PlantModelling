namespace PlantSimulator.Simulation.Cells.Storage
{
    public abstract class Fluid : IFluid
    {
        public float Amount { get; protected set; }

        protected Fluid(float amount)
        {
            Amount = amount;
        }
    }
}