using System;
using NUnit.Framework;
using PlantSimulator;
using PlantSimulator.Outputs;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulatorTests.IntegrationTests.Outputs
{
    [TestFixture]
    public class SimulationStateFactoryIntegrationTests
    {

        private ISimulationStateFactory stateFactory;

        private IPlant plant;

        private SimulationStateData stateData;

        [SetUp]
        public void Setup()
        {
            stateFactory = new SimulationStateFactory();
            plant = TestPlant.CreatePlant();
            stateData = new SimulationStateData {Id = Guid.NewGuid().ToString(), SimulationTimer = 0};
        }

        [Test]
        public void Create_WhenGivenTestPlant_ShouldCreateState()
        {
            var state = stateFactory.Create(plant, stateData);

            Assert.NotNull(state);
        }
    }
}