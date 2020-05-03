using System.Runtime.CompilerServices;
using Microsoft.Extensions.Hosting;

namespace PlantSimulator.Simulation.Geometry
{
    public interface ICellGeometry
    {
        public IVertex TopCenter { get; }

        public IVertex BottomCenter { get; }

        public IFace Face { get; }

        public float Length { get; }

        public bool Equals(ICellGeometry geometry)
        {
            return TopCenter.Equals(geometry.TopCenter) && BottomCenter.Equals(geometry.BottomCenter) &&
                   Face.Equals(geometry.Face);
        }
    }
}