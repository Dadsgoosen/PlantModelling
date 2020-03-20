namespace PlantSimulator.Simulation.Geometry
{
    public interface ICellGeometry
    {
        public IVertex TopCenter { get; }

        public IVertex BottomCenter { get; }

        public IFace Face { get; }

        public float Length { get; }
    }
}