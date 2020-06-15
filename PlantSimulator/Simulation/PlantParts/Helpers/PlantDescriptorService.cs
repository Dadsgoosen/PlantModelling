using System.Numerics;

namespace PlantSimulator.Simulation.PlantParts.Helpers
{
    public class PlantDescriptorService : IPlantDescriptorService
    {
        public IPlantPartDescriptor Describe(IPlantPart part)
        {
            var highest = Vector2.Zero;
            var lowest = Vector2.Zero;
            var widest = new[] { Vector2.Zero, Vector2.Zero };

            foreach (var cell in part.Cells)
            {
                var top = cell.Geometry.TopCenter;
                var bottom = cell.Geometry.BottomCenter;

                if (top.Y > highest.Y)
                {
                    highest = new Vector2(top.X, top.Y);
                }

                if (bottom.Y < lowest.Y)
                {
                    lowest = new Vector2(bottom.X, bottom.Y);
                }

                if (top.X > widest[1].X)
                {
                    widest[1] = new Vector2(top.X, top.Y);
                }

                if (bottom.X < widest[0].X)
                {
                    widest[0] = new Vector2(bottom.X, bottom.Y);
                }
            }

            var height = Vector2.Distance(highest, lowest);

            var thickness = Vector2.Distance(widest[0], widest[1]);

            return new PlantPartDescriptor(height, thickness);
        }
    }
}