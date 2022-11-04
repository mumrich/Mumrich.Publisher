using System.Collections.Generic;

using Mumrich.SpaDevMiddleware.Contracts;
using Mumrich.SpaDevMiddleware.Models;

namespace Mumrich.Publisher.Host
{
  public class AppSettings : ISpaDevServerSettings
  {
    public Dictionary<string, SpaSettings> SinglePageApps { get; set; }
    public string SpaRootPath { get; set; }
  }
}