using System.Threading.Tasks;

namespace Html2ImageConverter.Services.Interfaces
{
    public interface IProcessService
    {
        Task StartProcess(string filename, string arguments);
    }
}
