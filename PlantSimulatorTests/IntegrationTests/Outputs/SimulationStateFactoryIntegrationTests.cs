using System;
using System.Linq;
using NUnit.Framework;
using PlantSimulator;
using PlantSimulator.Outputs;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.PlantParts;

namespace PlantSimulatorTests.IntegrationTests.Outputs
{
    [TestFixture]
    public class SimulationStateFactoryIntegrationTests
    {
        private string id;

        private ISimulationStateFactory stateFactory;

        private IPlant plant;

        private SimulationStateSnapshot stateData;

        [SetUp]
        public void Setup()
        {
            id = Guid.NewGuid().ToString();
            stateFactory = new SimulationStateFactory();
            plant = TestPlant.CreatePlant();
            stateData = new SimulationStateSnapshot(100);
        }

        [Test]
        public void Create_WhenGivenTestPlant_ShouldCreateState()
        {
            var state = stateFactory.Create(id, plant, stateData);
            Assert.NotNull(state);
            Assert.AreEqual(state.Id, id);
            Assert.AreEqual(state.Plant.ShootSystem.Count(), 1);
            Assert.AreEqual(state.Plant.RootSystem.Count(), 1);
        }
    }
}