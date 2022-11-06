using System.Drawing;
using System.IO;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using SkiaSharp;

using Svg;
using Svg.Skia;

namespace Mumrich.Publisher.Host.Controllers
{
  [Route("[controller]")]
  public class SvgController : ControllerBase
  {
    [HttpGet]
    public FileStreamResult Get()
    {
      var svgDoc = GetSvgDocument();

      MemoryStream memoryStream = new();
      svgDoc.Write(memoryStream);

      memoryStream.Seek(0, SeekOrigin.Begin);

      return File(memoryStream, MediaTypeNames.Text.Html);
    }

    [HttpGet("json")]
    public SvgDocument GetJson()
    {
      return GetSvgDocument();
    }

    [HttpGet("pdf")]
    public FileStreamResult GetPdf()
    {
      var svgDoc = GetSvgDocument(false);

      var skSvg = new SKSvg();

      skSvg.FromSvgDocument(svgDoc);

      MemoryStream memoryStream = new();

      ToPdf(skSvg.Picture, memoryStream, SKColors.Empty, 1f, 1f, 300);

      memoryStream.Seek(0, SeekOrigin.Begin);

      return File(memoryStream, MediaTypeNames.Application.Pdf);
    }

    private static void Draw(SKPicture skPicture, SKColor background, float scaleX, float scaleY, SKCanvas skCanvas)
    {
      skCanvas.DrawColor(background);
      skCanvas.Save();
      skCanvas.Scale(scaleX, scaleY);
      skCanvas.DrawPicture(skPicture);
      skCanvas.Restore();
    }

    private static SvgDocument GetSvgDocument(bool isInEditMode = true)
    {
      var svgDoc = new SvgDocument
      {
        Width = 500,
        Height = 500,
        ViewBox = new SvgViewBox(-250, -250, 500, 500)
      };

      var group = new SvgGroup();

      svgDoc.Children.Add(group);
      group.Children.Add(new SvgCircle
      {
        Radius = 100,
        Fill = new SvgColourServer(Color.Red),
        Stroke = new SvgColourServer(Color.Black),
        StrokeWidth = 2
      });

      const string text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Sed mollis mollis mi ut ultricies. Nullam magna ipsum, porta vel dui convallis, rutrum imperdiet eros. Aliquam erat volutpat.";

      if (isInEditMode)
      {
        var foreignObject = new SvgForeignObject();

        foreignObject.CustomAttributes.Add("x", "20");
        foreignObject.CustomAttributes.Add("y", "20");
        foreignObject.CustomAttributes.Add("width", "160");
        foreignObject.CustomAttributes.Add("height", "160");
        foreignObject.Children.Add(new NonSvgElement("div", "http://www.w3.org/1999/xhtml")
        {
          Content = text
        });

        group.Children.Add(foreignObject);
      }
      else
      {
        var svgText = new SvgText(text);

        svgText.CustomAttributes.Add("x", "20");
        svgText.CustomAttributes.Add("y", "20");
        svgText.CustomAttributes.Add("width", "160");
        svgText.CustomAttributes.Add("height", "160");

        group.Children.Add(svgText);
      }

      var svgImage = new SvgImage()
      {
        X = 30,
        Y = 30,
        Width = 200,
        Height = 150,
        Href = "https://localhost:7284/images/Large-Sample-png-Image-download-for-Testing.png"
      };

      svgDoc.Children.Add(svgImage);

      return svgDoc;
    }

    private static bool ToPdf(SKPicture skPicture, Stream stream, SKColor background, float scaleX, float scaleY, float rasterDpi = SKDocument.DefaultRasterDpi)
    {
      var width = skPicture.CullRect.Width * scaleX;
      var height = skPicture.CullRect.Height * scaleY;
      if (width <= 0 || height <= 0)
      {
        return false;
      }
      using var skDocument = SKDocument.CreatePdf(stream, rasterDpi);
      using var skCanvas = skDocument.BeginPage(width, height);
      Draw(skPicture, background, scaleX, scaleY, skCanvas);
      skDocument.Close();
      return true;
    }
  }
}