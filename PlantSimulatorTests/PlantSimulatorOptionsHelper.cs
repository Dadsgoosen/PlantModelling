using System;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Options;

namespace PlantSimulatorTests
{
    internal static class PlantSimulatorOptionsHelper
    {
        public static IPlantSimulatorOptionsService CreateOptionsService()
        {
            var options = new PlantSimulationOptions
            {
                Id = Guid.NewGuid().ToString(),
                Simulation = new SimulationOptions
                {
                    TickTime = 0,
                    RandomSeed = 3,
                    TickEventTime = 1
                },
                Environment = new SimulationEnvironmentOptions
                {
                    LightSource = new Vertex(0, 1000, 0), 
                    Temperature = 21f
                },
                Plant = new SimulationPlantOptions
                {
                    PetioleLength = new Range<int>(5, 5),
                    NewStemWidth = new Range<int>(21, 21),
                    Branches = new Range<int>(0, 0),
                    PetiolesPerNode = new Range<int>(1, 2),
                    StemsPerNode = new Range<int>(0, 0),
                    NewPetioleWidth = new Range<int>(5, 5),
                    InitialStemWidth = 21,
                    SubBranches = new Range<int>(0, 0),
                    MaxInternodeLength = new Range<float>(50, 60),
                    LeafsPerNode = new Range<int>(0, 0),
                    NewNodeTickCount = new Range<int>(50, 50),
                    GrowthRange = new Range<float>(.5f, .5f),
                    NewNodeInternodeLength = new Range<float>(0, 0),
                    TerminalHeight = new Range<float>(50f, 50f)
                },
                Cell = new SimulationCellOptions
                {
                    ParenchymaCellSize = 10,
                    CollenchymaCellSize = 10,
                    PhloemCellSize = 10,
                    SclerenchymaCellSize = 10,
                    XylemCellSize = 10
                },
            };

            return new PlantSimulatorOptionsService { Options = options };
        }
    }
}