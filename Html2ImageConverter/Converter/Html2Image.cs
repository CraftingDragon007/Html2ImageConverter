using Html2ImageConverter.Converter.Interfaces;
using Html2ImageConverter.Enums;
using Html2ImageConverter.Helpers;
using Html2ImageConverter.Options;
using Html2ImageConverter.Services;
using Html2ImageConverter.Services.Interfaces;
using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;

namespace Html2ImageConverter.Converter
{
    /// <summary>
    /// Html Converter. Converts HTML string and URLs to image bytes
    /// </summary>
    public class Html2Image : IHtml2Image
    {
        private static string toolFilename = "wkhtmltoimage";
        private static string _directory;
        private static string toolFilepath;
        protected ConverterImageType ConverterImageType;
        private IProcessService _processService;
        private IFileService _fileService;
        public Html2Image(IProcessService? processService = null, IFileService? fileService = null)
        {
            _directory = AppContext.BaseDirectory;
            _processService = processService ?? new ProcessService();
            _fileService = fileService ?? new FileService();

            //Check on what platform we are
            if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows))
            {
                toolFilepath = Path.Combine(_directory, toolFilename + ".exe");

                if (!File.Exists(toolFilepath))
                {
                    var assembly = typeof(Html2Image).GetTypeInfo().Assembly;
                    var type = typeof(Html2Image);
                    var ns = type.Namespace.Split(".")[0];
                    string[] names = assembly.GetManifestResourceNames();
                    using (var resourceStream = assembly.GetManifestResourceStream($"{ns}.{toolFilename}.exe"))
                    using (var fileStream = File.OpenWrite(toolFilepath))
                    {
                        resourceStream.CopyTo(fileStream);
                    }
                }
            }
            else if (System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Linux))
            {
                //Check if wkhtmltoimage package is installed on this distro in using which command
                Process process = Process.Start(new ProcessStartInfo()
                {
                    CreateNoWindow = true,
                    UseShellExecute = false,
                    WorkingDirectory = "/bin/",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    FileName = "/bin/bash",
                    Arguments = "-c \"which wkhtmltoimage\""

                });
                string answer = process.StandardOutput.ReadToEnd();
                process.WaitForExit();

                if (!string.IsNullOrEmpty(answer) && answer.Contains("wkhtmltoimage"))
                {
                    toolFilepath = "wkhtmltoimage";
                }
                else
                {
                    throw new Exception("wkhtmltoimage does not appear to be installed on this linux system according to which command; go to https://wkhtmltopdf.org/downloads.html");
                }
            }
            else
            {
                //OSX not implemented
                throw new Exception("OSX Platform not implemented yet");
            }
        }

        /// <summary>
        /// Converts HTML string to image
        /// </summary>
        /// <param name="html">HTML string</param>
        /// <param name="imageOptions"></param>
        /// <returns></returns>
        public async Task<byte[]> HtmlStringToImage(string html, ImageOptions? imageOptions = null)
        {
            var filename = Path.Combine(_directory, $"{Guid.NewGuid()}.html");

            await _fileService.WriteFileAsync(filename, html);

            var bytes = await UrlToImage(filename, imageOptions);
            File.Delete(filename);
            return bytes;
        }

        /// <summary>
        /// Converts HTML page to image
        /// </summary>
        /// <param name="url">Valid http(s):// URL</param>
        /// <param name="imageOptions"></param>
        /// <returns></returns>
        public async Task<byte[]> UrlToImage(string url, ImageOptions? imageOptions = null)
        {
            imageOptions ??= new ImageOptions();

            var imageFormat = imageOptions.Format.ToString().ToLower();

            var filename = Path.Combine(_directory, $"{Guid.NewGuid()}.{imageFormat}");

            string args;

            if (Utils.IsLocalPath(url))
            {
                args = $"--quality {imageOptions.Quality} --width {imageOptions.Width} -f {imageFormat} \"{url}\" \"{filename}\"";
            }
            else
            {
                args = $"--quality {imageOptions.Quality} --width {imageOptions.Width} -f {imageFormat} {url} \"{filename}\"";
            }

            await _processService.StartProcess(toolFilepath, args);

            if (await _fileService.ExistsAsync(filename))
            {
                var bytes = await _fileService.ReadFileAsync(filename);
                await _fileService.DeleteAsync(filename);
                return bytes;
            }

            throw new Exception("Something went wrong. Please check input parameters");
        }

    }
}
