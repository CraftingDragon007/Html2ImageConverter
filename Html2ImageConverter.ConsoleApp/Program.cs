using Html2ImageConverter.Converter;
using Html2ImageConverter.Enums;
using Html2ImageConverter.Options;

var converter = new Html2Image();

var html = @$"<!DOCTYPE html>
            <html>
                <head>
                    <title>Example</title>
                </head>
                <body>
                    <div><strong>Hello</strong> World!</div>
                    <p>This is an example of a simple HTML page with one paragraph.</p>
                </body>
            </html>";

var htmlBytes = await converter.HtmlStringToImage(html);

await File.WriteAllBytesAsync("D:\\helloWorld.png", htmlBytes);

// From URL
var urlBytes = await converter.UrlToImage("http://google.com",
    new ImageOptions
    {
        Format = ConverterImageType.Jpeg,
        Width = 800,
        Quality = 80
    });

await File.WriteAllBytesAsync("D:\\image.jpeg", urlBytes);