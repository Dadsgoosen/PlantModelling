﻿using PlantSimulator.Simulation;
using PlantSimulator.Simulation.PlantParts;
using SimulationState = PlantSimulator.Outputs.Models.SimulationState;

namespace PlantSimulator.Outputs
{
    public interface ISimulationStateFactory
    {
        public SimulationState Create(string id, IPlant plant, SimulationStateSnapshot data);
    }
}