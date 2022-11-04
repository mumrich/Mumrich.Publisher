using System.Drawing;
using System.IO;
using System.Net.Mime;

using Microsoft.AspNetCore.Mvc;

using Svg;

namespace Mumrich.Publisher.Host.Controllers
{
  [Route("[controller]")]
  public class SvgController : ControllerBase
  {
    [HttpGet]
    public FileStreamResult Get()
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

      MemoryStream memoryStream = new();
      svgDoc.Write(memoryStream);

      memoryStream.Seek(0, SeekOrigin.Begin);

      return File(memoryStream, MediaTypeNames.Text.Html);
    }
  }
}