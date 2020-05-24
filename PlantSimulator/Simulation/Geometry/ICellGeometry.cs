using System.Numerics;
using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;

namespace PlantSimulator.Simulation.Geometry
{
    public interface ICellGeometry
    {
        public Vector3 TopCenter { get; set; }

        public Vector3 BottomCenter { get; set; }

        public IFace Face { get; }

        public float Length { get; }

        public bool Equals(ICellGeometry geometry)
        {
            return TopCenter.Equals(geometry.TopCenter) && BottomCenter.Equals(geometry.BottomCenter) &&
                   Face.Equals(geometry.Face);
        }
    }
}