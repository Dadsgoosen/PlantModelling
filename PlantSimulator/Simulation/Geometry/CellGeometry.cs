using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public class CellGeometry : ICellGeometry
    {
        public IVertex TopCenter { get; }

        public IVertex BottomCenter { get; }

        public IFace Face { get; }

        public float Length => TopCenter.Length(BottomCenter);

        public CellGeometry(IVertex topCenter, IVertex bottomCenter, IFace faceGeometry)
        {
            TopCenter = topCenter;
            BottomCenter = bottomCenter;
            Face = faceGeometry;
        }

    }
}