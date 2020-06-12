using System;
using System.Collections.Generic;
using System.Drawing;
using System.Numerics;
using System.Runtime.CompilerServices;
using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Geometry
{
    public class CellCollisionDetection : ICellCollisionDetection
    {
        private readonly IGeometryHelper helper;

        public CellCollisionDetection(IGeometryHelper helper)
        {
            this.helper = helper;
        }

        public bool Colliding(IPlantCell a, IPlantCell b, bool allowOnLine)
        {
            if (!IsWithinHeight(a.Geometry, b.Geometry)) return false;

            var aFace = a.Geometry.Face.Points;
            var bFace = b.Geometry.Face.Points;

            var inside = IsInsidePolygon(aFace, bFace);

            if (inside && allowOnLine)
            {
                var isOnLine = IsOnPolygonLine(aFace, helper.CreateFacePairs(bFace));

                return !isOnLine;
            }

            return inside;
        }

        public bool Neighbors(IPlantCell a, IPlantCell b, bool allowInside)
        {
            var aGeo = a.Geometry;
            var bGeo = b.Geometry;

            if (!IsWithinHeight(aGeo, bGeo)) return false;

            var aFace = aGeo.Face.Points;
            var bFace = bGeo.Face.Points;
            
            if (allowInside)
            {
                return IsInsidePolygon(aFace, bFace);
            }

            return IsOnPolygonLine(aFace, helper.CreateFacePairs(bFace));
        }

        private bool IsWithinHeight(ICellGeometry a, ICellGeometry b)
        {
            var topWithin = helper.IsWithinHeight(a.TopCenter, b.TopCenter, b.BottomCenter, true);
            var bottomWithin = helper.IsWithinHeight(a.BottomCenter, b.TopCenter, b.BottomCenter, true);
            return topWithin || bottomWithin;
        }

        private bool IsInsidePolygon(Vector2[] aFace, Vector2[] bFace)
        {
            for (int i = 0; i < aFace.Length; i++)
            {
                if (helper.IsInsidePolygon(aFace[i], bFace))
                {
                    return true;
                }
            }

            return false;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private bool IsOnPolygonLine(Vector2[] aFace, Vector2[][] linePairs)
        {
            for (int i = 0; i < aFace.Length; i++)
            {
                for (int j = 0; j < linePairs.Length; j++)
                {
                    if (helper.IsOnLine(aFace[i], linePairs[j]))
                    {
                        return true;
                    }
                }
            }

            return false;
        }
    }
}