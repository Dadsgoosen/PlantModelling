namespace PlantSimulator.Simulation.Geometry
{
    public interface IVertex
    {
        public float X { get; }

        public float Y { get; }

        void MoveTo(IVertex location);
    }
}