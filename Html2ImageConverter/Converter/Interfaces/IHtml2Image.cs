using Html2ImageConverter.Options;
using System.Threading.Tasks;

namespace Html2ImageConverter.Converter.Interfaces
{
    public interface IHtml2Image
    {
        Task<byte[]> UrlToImage(string url, ImageOptions? imageOptions = null);
        Task<byte[]> HtmlStringToImage(string html, ImageOptions? imageOptions = null);
    }
}
