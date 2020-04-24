using System.Collections.Generic;

namespace PlantSimulator.Outputs.Models
{
    /// <summary>
    /// Data Access Object for a Plant Model.
    /// </summary>
    public class PlantModelState
    {
        /// <summary>
        /// An ordered list of Plant Node Model Descriptors for
        /// the Shoot System that can be used to draw the plant.
        /// </summary>
        public IEnumerable<PlantNodeModelState> ShootSystem { get; set; }

        /// <summary>
        /// An ordered list of Plant Node Model Descriptors for
        /// the Root System that can be used to draw the plant.
        /// </summary>
        public IEnumerable<PlantNodeModelState> RootSystem { get; set; }
    }
}