using System.Numerics;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public class CellGeometry : ICellGeometry
    {
        public Vector3 TopCenter { get; set; }

        public Vector3 BottomCenter { get; set; }

        public IFace Face { get; }

        public float Length => Vector3.Distance(TopCenter, BottomCenter);

        public CellGeometry(Vector3 topCenter, Vector3 bottomCenter, IFace faceGeometry)
        {
            TopCenter = topCenter;
            BottomCenter = bottomCenter;
            Face = faceGeometry;
        }

    }
}