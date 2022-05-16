using System.Threading.Tasks;

namespace Html2ImageConverter.Services.Interfaces
{
    public interface IFileService
    {
        Task<bool> ExistsAsync(string filePath);
        Task DeleteAsync(string fullPath);
        Task WriteFileAsync(string fullPath, string content);
        Task<byte[]> ReadFileAsync(string fullPath);

    }
}
