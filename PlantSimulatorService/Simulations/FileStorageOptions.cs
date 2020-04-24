using System.Text.Json;

namespace PlantSimulatorService.Simulations
{
    /// <summary>
    /// Options for the <see cref="SimulationFileStorage"/>
    /// </summary>
    public class FileStorageOptions
    {
        /// <summary>
        /// Computed field for the <see cref="Path"/> property
        /// </summary>
        private string path;

        /// <summary>
        /// The path to store the simulations in
        /// </summary>
        public string Path
        {
            get => path;
            set => path = SetPath(value);
        }

        /// <summary>
        /// Helper function to make sure that the path ends with
        /// directory separator character for the specific platform
        /// </summary>
        /// <param name="path">The new path</param>
        /// <returns>The path with the added </returns>
        private static string SetPath(string path)
        {
            char[] separators =
            {
                System.IO.Path.DirectorySeparatorChar,
                System.IO.Path.AltDirectorySeparatorChar
            };

            if (!path.EndsWith(separators[0]) || !path.EndsWith(separators[1]))
            {
                path += separators[0];
            }

            return path;
        }
    }


}