using Html2ImageConverter.Services.Interfaces;
using System.IO;
using System.Threading.Tasks;

namespace Html2ImageConverter.Services
{
    class FileService : IFileService
    {
        public async Task<bool> ExistsAsync(string filePath) => await Task.Run(() => File.Exists(filePath));

        public async Task WriteFileAsync(string fullPath, string content)
        {
            await File.WriteAllTextAsync(fullPath, content);
        }

        public async Task<byte[]> ReadFileAsync(string fullPath)
        {
            return await File.ReadAllBytesAsync(fullPath);
        }

        public Task DeleteAsync(string fullPath)
        {
            if (string.IsNullOrEmpty(fullPath)) return Task.FromResult(true);

            File.Delete(fullPath);

            return Task.FromResult(true);
        }
    }
}
