using System;
using PlantSimulator.Helpers;
using PlantSimulator.Simulation.Geometry;
using PlantSimulator.Simulation.Options;

namespace PlantSimulator.Simulation.Plants.Fluids
{
    public static class FluidsPlantOptions
    {
        public static IPlantSimulatorOptions CreateOptions()
        {
            return new PlantSimulationOptions
            {
                Id = Guid.NewGuid().ToString(),
                Simulation = new SimulationOptions
                {
                    TickTime = 50,
                    RandomSeed = 3,
                    TickEventTime = 24,
                    StopAtTick = 1320
                },
                Cell = new SimulationCellOptions
                {
                    ParenchymaCellSize = .5f,
                    CollenchymaCellSize = .5f,
                    PhloemCellSize = .5f,
                    SclerenchymaCellSize = .5f,
                    XylemCellSize = .5f
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
                    MaxInternodeLength = new Range<float>(7, 8),
                    LeafsPerNode = new Range<int>(0, 0),
                    NewNodeTickCount = new Range<int>(int.MaxValue, int.MaxValue),
                    GrowthRange = new Range<float>(0.1f, 0.1f),
                    NewNodeInternodeLength = new Range<float>(3, 4),
                    TerminalHeight = new Range<float>(90, 90),
                    Axil = 45,
                    RootTickStopGrowth = new Range<int>(int.MaxValue, int.MaxValue),
                    TerminalRootLength = new Range<float>(30, 30),
                    NewRootLength = new Range<float>(100, 100),
                    NewRootTick = new Range<int>(9999, 9999)
                }
            };
        }
    }
}