using System;
using System.Linq;
using NUnit.Framework;
using PlantSimulator;
using PlantSimulator.Outputs;
using PlantSimulator.Simulation;
using PlantSimulator.Simulation.PlantParts;
using PlantSimulator.Simulation.PlantParts.Helpers;

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
            stateFactory = new SimulationStateFactory(new PlantDescriptorService());
            plant = TestPlant.CreatePlant();
            stateData = new SimulationStateSnapshot(100);
        }

        [Test]
        public void Create_WhenGivenTestPlant_ShouldCreateState()
        {
            var state = stateFactory.Create(id, plant, stateData);
            Assert.NotNull(state);
            Assert.AreEqual(id, state.Id);
            Assert.AreEqual(1, state.Plant.ShootSystem.Count());
            Assert.AreEqual(1, state.Plant.RootSystem.Count());
        }
    }
}