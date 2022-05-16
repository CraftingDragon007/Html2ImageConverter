using Html2ImageConverter.Services.Interfaces;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Html2ImageConverter.Services
{
    class ProcessService : IProcessService
    {
        public async Task StartProcess(string filename, string arguments)
        {
            await Task.Run(() =>
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo(filename, arguments)
                    {
                        WindowStyle = ProcessWindowStyle.Hidden,
                        CreateNoWindow = true,
                        UseShellExecute = false,
                        RedirectStandardError = true
                    }
                };

                process.Start();
                process.WaitForExit();
            });
        }
    }
}
