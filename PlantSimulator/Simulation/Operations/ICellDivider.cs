using PlantSimulator.Simulation.Cells;

namespace PlantSimulator.Simulation.Operations
{
    /// <summary>
    /// Interface for cell division operations
    /// </summary>
    public interface ICellDivider
    {
        /// <summary>
        /// Determines whether a cell should divide
        /// </summary>
        /// <param name="cell">The cell to determine should divide</param>
        /// <returns>
        /// Boolean value representing whether the cell should divide. <br/>
        /// True means the cell should divide, false means it should not.
        /// </returns>
        bool ShouldDivide(IPlantCell cell);

        /// <summary>
        /// Divide the cell into two new daughter cells
        /// </summary>
        /// <param name="cell">The cell to divide</param>
        /// <returns>An array containing the new daughter cells</returns>
        IPlantCell[] Divide(IPlantCell cell);
    }
}