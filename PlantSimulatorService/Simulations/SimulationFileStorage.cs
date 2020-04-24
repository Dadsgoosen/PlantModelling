using System;
using System.IO;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Simulations
{
    public class SimulationFileStorage : ISimulationStorage
    {
        private readonly IOptionsMonitor<FileStorageOptions> options;

        private readonly IFileHandler<SimulationState> fileHandler;

        public SimulationFileStorage(IOptionsMonitor<FileStorageOptions> options, IFileHandler<SimulationState> fileHandler)
        {
            this.options = options;
            this.fileHandler = fileHandler;
        }

        public async Task StoreSimulationAsync(SimulationState state, CancellationToken cancellationToken = default)
        {
            string path = CreatePath(state.Id);

            await fileHandler.WriteFile(path, state, cancellationToken);
        }

        public async Task<SimulationState> GetSimulationState(string id, CancellationToken cancellationToken = default)
        {
            string path = CreatePath(id);

            using var stream = ReadFile(path, cancellationToken);

            return await DeSerializeState(await stream, cancellationToken);
        }

        public async Task<SimulationState[]> GetSimulationStates(CancellationToken cancellationToken = default)
        {
            string[] files = fileHandler.GetFilesInDirectory(options.CurrentValue.Path);

            SimulationState[] states = new SimulationState[files.Length];

            for (int i = 0; i < files.Length; i++)
            {
                using var stream = ReadFile(files[i], cancellationToken);

                states[i] = await DeSerializeState(await stream, cancellationToken);
            }

            return states;
        }

        public bool DeleteSimulationState(string id)
        {
            return fileHandler.DeleteFile(CreatePath(id));
        }

        private async Task<Stream> ReadFile(string path, CancellationToken cancellationToken)
        {
            byte[] jsonBytes = await fileHandler.ReadFileAsync(path, cancellationToken);

            return new MemoryStream(jsonBytes, false);
        }

        private async Task<SimulationState> DeSerializeState(Stream stream, CancellationToken cancellationToken)
        {
            return await JsonSerializer.DeserializeAsync<SimulationState>(stream, cancellationToken: cancellationToken);
        }

        private string CreatePath(string id)
        {
            string path = options.CurrentValue.Path;

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            return path + id + ".json";
        }
    }
}