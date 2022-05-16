# Html2ImageConverter
.NET Core wkhtmltoimage wrapper

Specification:

* support dependencyInjection
* asynchronous
* .net standard 2.1


# Dependency Injection
```
public void ConfigureServices(IServiceCollection services)
{
    services.Html2ImageConverter();
}
```
# Example DI
```
using HtmlToImageConverter.Converter.Interfaces;
using HtmlToImageConverter.Enums;

public class Example
{
    private readonly IHtml2Image html2Image;
    
    public Example(IHtml2Image html2Image)
    {
        this.html2Image = html2Image;
    }

    private async Task<byte[]> HtmlToJpg(int quality = 100)
    {

        var html = "<html><body><h1>Hello world!</h1></body></html>";

        return await _htmlToImage.HtmlStringToImage(html, new Html2ImageConverter.Options.ImageOptions
        {
            Format = ConverterImageType.Jpg,
            Quality = quality,
            Width = 800
        });
    }

    private async Task<byte[]> UrlToJpg(int quality = 100)
    {
        return await _html2Image.HtmlStringToImage(html, new Html2ImageConverter.Options.ImageOptions
        {
            Format = ConverterImageType.Jpg,
            Quality = quality > 0 ? quality : 100,
            Width = 850
        });
    }    
}

```

# Example without DI

```
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

```
