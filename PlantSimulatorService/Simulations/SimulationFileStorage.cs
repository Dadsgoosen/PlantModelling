using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using PlantSimulatorService.Simulations.Model;

namespace PlantSimulatorService.Simulations
{
    public class SimulationFileStorage : ISimulationStorage
    {
        private readonly IOptionsMonitor<FileStorageOptions> options;

        private readonly ILogger<SimulationFileStorage> logger;

        private readonly IFileHandler<SimulationState> fileHandler;

        public SimulationFileStorage(
            IOptionsMonitor<FileStorageOptions> options, 
            ILogger<SimulationFileStorage> logger,
            IFileHandler<SimulationState> fileHandler)
        {
            this.options = options;
            this.logger = logger;
            this.fileHandler = fileHandler;
        }

        public async Task StoreSimulationAsync(SimulationState state, CancellationToken cancellationToken = default)
        {
            string path = CreatePath(state.Id + "_" + state.SimulationTime);

            await fileHandler.WriteFile(path, state, cancellationToken);
        }

        public async Task<IDictionary<string, SimulationState>> GetSimulationState(string id, CancellationToken cancellationToken = default)
        {
            var states = new Dictionary<string, SimulationState>();

            var fileNames = fileHandler.GetFilesInDirectory(options.CurrentValue.Path);

            foreach (var fileName in fileNames)
            {
                if (!fileName.Contains(id)) continue;
                
                var name = Path.GetFileNameWithoutExtension(fileName);

                var split = name.Split('_');

                var fileTime = split[^1];

                var added = states.TryAdd(fileTime,
                    await DeSerializeState(await ReadFile(fileName, cancellationToken), cancellationToken));

                if (!added)
                {
                    logger.LogWarning("Simulation {SimulationId} at {SimulationTime} was already added.", split[0], fileTime);
                }
            }

            return states;
        }

        public async Task<SimulationState[]> GetSimulationStates(CancellationToken cancellationToken = default)
        {
            string[] files = fileHandler.GetFilesInDirectory(options.CurrentValue.Path);

            IDictionary<string, SimulationState> states = new Dictionary<string, SimulationState>();

            foreach (var fileName in files)
            {
                var id = Path.GetFileNameWithoutExtension(fileName).Split('_')[0];

                if (states.ContainsKey(id)) continue;

                using var stream = ReadFile(fileName, cancellationToken);

                var state = await DeSerializeState(await stream, cancellationToken);

                states.Add(state.Id, state);
            }

            return states.Values.ToArray();
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