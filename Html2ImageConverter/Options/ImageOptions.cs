using Html2ImageConverter.Enums;

namespace Html2ImageConverter.Options
{
    public class ImageOptions
    {
        public int Width { get; set; } = 1024;
        public ConverterImageType Format { get; set; } = ConverterImageType.Jpg;
        public int Quality { get; set; } = 96;
    }
}