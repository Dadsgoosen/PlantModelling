using System;
using System.IO;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace PlantSimulatorService.Simulations
{
    public class JsonFileHandler<T> : IFileHandler<T>
    {
        public async Task WriteFile(string path, T body, CancellationToken token)
        {
            await using (StreamWriter writer = File.CreateText(path))
            {
                await JsonSerializer.SerializeAsync(writer.BaseStream, body, cancellationToken: token);
            }
        }

        public async Task<byte[]> ReadFileAsync(string path, CancellationToken cancellationToken = default)
        {
            try
            {
                return await File.ReadAllBytesAsync(path, cancellationToken);
            }
            catch (FileNotFoundException e)
            {
                ThrowError(e);
            }
            catch (DirectoryNotFoundException e)
            {
                ThrowError(e);
            }

            return null;
        }

        public string[] GetFilesInDirectory(string path)
        {
            return Directory.GetFiles(path, "*.json");
        }

        public bool DeleteFile(string path)
        {
            try
            {
                File.Delete(path);
            }
            catch (DriveNotFoundException)
            {
                return false;
            }
            return true;
        }

        private static void ThrowError(Exception e)
        {
            throw new FileNotFoundException("File at {path} could not be found", e);
        }
    }
}