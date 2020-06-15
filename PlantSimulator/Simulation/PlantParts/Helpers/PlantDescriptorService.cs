using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class PlantDescriptorService : IPlantDescriptorService
    {
        public IPlantPartDescriptor Describe(IPlantPart part)
        {
            var highest = new Vector3(float.MinValue);
            var lowest = new Vector3(float.MaxValue);
            var widest = new[] {new Vector3(float.MaxValue), new Vector3(float.MinValue)};

            foreach (var cell in part.Cells)
            {
                var top = cell.Geometry.TopCenter;
                var bottom = cell.Geometry.BottomCenter;

                if (top.Y > highest.Y)
                {
                    highest = top;
                }

                if (bottom.Y < lowest.Y)
                {
                    lowest = bottom;
                }

                if (top.X > widest[1].X)
                {
                    widest[1] = top;
                }

                if (bottom.X < widest[0].X)
                {
                    widest[0] = bottom;
                }
            }

            var height = Vector3.Distance(highest, lowest);

            var thickness = Vector3.Distance(widest[0], widest[1]);

            return new PlantPartDescriptor(highest, lowest, height, thickness);
        }
    }
}