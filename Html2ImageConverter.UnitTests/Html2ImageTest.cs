using Html2ImageConverter.Converter;
using Html2ImageConverter.Enums;
using Html2ImageConverter.Options;
using Html2ImageConverter.Services.Interfaces;
using Moq;
using NUnit.Framework;
using System.Threading.Tasks;

namespace Html2ImageConverter.UnitTests
{
    [TestFixture]
    public class HtmlToImageTest
    {
        private Html2Image _html2Image;
        private Mock<IProcessService> _processServiceMock;
        private Mock<IFileService> _fileServiceMock;

        [SetUp]
        public void Setup()
        {
            _processServiceMock = new Mock<IProcessService>();
            _fileServiceMock = new Mock<IFileService>();
            _html2Image = new Html2Image(_processServiceMock.Object, _fileServiceMock.Object);
        }

        [Test]
        public async Task Html2ImageConverter_FromHtmlStringAsync()
        {
            byte[] fileArray = new byte[10];

            var html = "<html><body><h1>Hello world!</h1></body></html>";

            _fileServiceMock.Setup(arg => arg.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _fileServiceMock.Setup(arg => arg.ReadFileAsync(It.IsAny<string>())).ReturnsAsync(fileArray);

            var result = await _html2Image.HtmlStringToImage(html);

            Assert.IsNotNull(result);
        }

        [Test]
        public async Task Html2ImageConverter_FromUrlAsync()
        {
            byte[] fileArray = new byte[10];

            _fileServiceMock.Setup(arg => arg.ExistsAsync(It.IsAny<string>())).ReturnsAsync(true);
            _fileServiceMock.Setup(arg => arg.ReadFileAsync(It.IsAny<string>())).ReturnsAsync(fileArray);

            var result = await _html2Image.UrlToImage("http://google.com", new ImageOptions
            {
                Format = ConverterImageType.Jpeg,
                Width = 800,
                Quality = 80
            });

            Assert.IsNotNull(result);
        }
    }
}