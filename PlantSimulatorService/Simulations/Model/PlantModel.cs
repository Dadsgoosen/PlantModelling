using System.Collections.Generic;

namespace PlantSimulatorService.Simulations.Model
{
    /// <summary>
    /// Data Access Object for a Plant Model.
    /// </summary>
    public class PlantModel
    {
        /// <summary>
        /// An ordered list of Plant Node Model Descriptors for
        /// the Shoot System that can be used to draw the plant.
        /// </summary>
        public IEnumerable<PlantNodeModel> ShootSystem { get; set; }

        /// <summary>
        /// An ordered list of Plant Node Model Descriptors for
        /// the Root System that can be used to draw the plant.
        /// </summary>
        public IEnumerable<PlantNodeModel> RootSystem { get; set; }
    }
}