using System;
using System.Collections.Generic;
using System.Numerics;
using PlantSimulator.Logging;
using PlantSimulator.Simulation.Cells;
using PlantSimulator.Simulation.Cells.Storage;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Operations.Carriers;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulator.Simulation.Operations.Transporters
{
    public abstract class FluidTransporter<TFluid> : ITransporter where TFluid : Fluid
    {
        private const float Tolerance = 10e-4f;

        protected readonly ICellCollisionDetection CollisionDetection;

        protected readonly IGeometryHelper GeometryHelper;

        protected readonly ICarrierCollection<TFluid> CarrierCollection;

        protected readonly ILoggerAdapter<FluidTransporter<TFluid>> Logger;

        protected FluidTransporter(ICellCollisionDetection collisionDetection, IGeometryHelper geometryHelper,
            ICarrierCollection<TFluid> carrierCollection, ILoggerAdapter<FluidTransporter<TFluid>> logger)
        {
            CollisionDetection = collisionDetection;
            GeometryHelper = geometryHelper;
            CarrierCollection = carrierCollection;
            Logger = logger;
        }

        public abstract void Transport(IPlantPart part);

        protected IPlantCell GetClosestCellTowards(IFluidCarrier<TFluid> carrier, IEnumerable<IPlantCell> neighbors)
        {
            var currentPoint = new Vector2(carrier.Current.Geometry.TopCenter.X, carrier.Current.Geometry.TopCenter.Z);

            float lowestDistance = float.MaxValue;

            IPlantCell closestCell = null;

            foreach (var neighbor in neighbors)
            {
                if (neighbor.Equals(carrier.ClosestTransportCell)) return neighbor;

                var neighboringPoint = new Vector2(neighbor.Geometry.TopCenter.X, neighbor.Geometry.TopCenter.Z);

                float dist = Vector2.Distance(currentPoint, neighboringPoint);

                if (dist < lowestDistance)
                {
                    lowestDistance = dist;
                    closestCell = neighbor;
                }
            }

            return closestCell;
        }

        protected IPlantCell MoveUpOrDown(IFluidCarrier<TFluid> carrier, IPlantPart part)
        {
            var cells = part.Cells;

            var currentTop = carrier.Current.Geometry.TopCenter.Y;

            var destTop = carrier.Destination.Geometry.BottomCenter.Y;

            IPlantCell next;

            if (IsWithinHeight(carrier.Current.Geometry, carrier.Destination.Geometry))
            {
                next = MoveTowardsDestination(carrier, cells);
            }
            else if (currentTop >= destTop)
            {
                next = FindUpOrDown(false, carrier.Current, part);
            }
            else
            {
                next = FindUpOrDown(true, carrier.Current, part);
            }

            return next;
        }

        protected bool MoveCarrier(IFluidCarrier<TFluid> carrier, IPlantPart part)
        {
            var cells = part.Cells;

            var current = carrier.Current;

            var neighbors = GetNeighboringCells(current, cells);

            IPlantCell next;

            if (carrier.Current.Geometry.TopCenter.Equals(carrier.Destination.Geometry.TopCenter))
            {
                return true;
            }

            if (carrier.IsInTransportCell)
            {
                next = MoveUpOrDown(carrier, part);
            }
            else
            {
                next = GetClosestCellTowards(carrier, neighbors);
            }

            if (next != null)
            {
                carrier.Current = next;
            }

            return false;
        }

        protected IPlantCell FindUpOrDown(bool up, IPlantCell cell, IPlantPart part)
        {
            foreach (var c in part.Cells)
            {
                if (c.CellType == cell.CellType && CollisionDetection.Neighbors(cell, c, true))
                {
                    if (up && cell.Geometry.TopCenter.Y <= c.Geometry.BottomCenter.Y)
                    {
                        return c;
                    }
                    if(!up && cell.Geometry.BottomCenter.Y >= c.Geometry.TopCenter.Y)
                    {
                        return c;
                    }
                }
            }

            // If we end up here, that means that there are no cells above or below the current cell
            // which could mean that it must go to next plant part connection
            return FindInConnection(up, cell, part);
        }

        private IPlantCell FindInConnection(bool up, IPlantCell cell, IPlantPart part)
        {
            foreach (var conn in part.Connections)
            {
                FindUpOrDown(up, cell, conn);
            }

            return null;
        }

        protected IPlantCell MoveTowardsDestination(IFluidCarrier<TFluid> carrier, IEnumerable<IPlantCell> cells)
        {
            IPlantCell next = null;

            float lowestDistance = float.MaxValue;

            foreach (var c in cells)
            {
                if (c.Equals(carrier.Destination))
                {
                    return c;
                }
                
                float dist = Vector3.Distance(carrier.Current.Geometry.TopCenter, c.Geometry.TopCenter);

                if (dist < lowestDistance)
                {
                    next = c;
                }
            }

            return next;
        }
        
        private bool IsWithinHeight(ICellGeometry a, ICellGeometry b)
        {
            var topWithin = GeometryHelper.IsWithinHeight(a.TopCenter, b.TopCenter, b.BottomCenter, true);
            var bottomWithin = GeometryHelper.IsWithinHeight(a.BottomCenter, b.TopCenter, b.BottomCenter, true);
            return topWithin || bottomWithin;
        }

        private IEnumerable<IPlantCell> GetNeighboringCells(IPlantCell cell, IEnumerable<IPlantCell> cells)
        {
            var neighbors = new List<IPlantCell>();

            foreach (var c in cells)
            {
                if (CollisionDetection.Neighbors(cell, c, true))
                {
                    neighbors.Add(c);
                }
            }

            return neighbors;
        }
    }
}