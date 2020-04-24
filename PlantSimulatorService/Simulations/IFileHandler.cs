using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulatorService.Simulations
{
    public interface IFileHandler<in T>
    {
        public Task WriteFile(string path, T body, CancellationToken token);

        public Task<byte[]> ReadFileAsync(string path, CancellationToken cancellationToken = default);

        public string[] GetFilesInDirectory(string path);

        public bool DeleteFile(string path);
    }
}