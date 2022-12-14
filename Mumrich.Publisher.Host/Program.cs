using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Mumrich.SpaDevMiddleware.Extensions;

namespace Mumrich.Publisher.Host
{
  public static class Program
  {
    public static void Main(string[] args)
    {
      var builder = WebApplication.CreateBuilder(args);
      var appSettings = builder.Configuration.Get<AppSettings>();

      builder.Services.AddControllersWithViews();
      builder.Services.AddCors();
      builder.RegisterSinglePageAppDevMiddleware(appSettings);

      var app = builder.Build();

      app.UseStaticFiles();
      app.UseHttpsRedirection();
      app.UseRouting();
      app.UseCors();
      app.MapControllers();
      app.MapSinglePageApps(appSettings);

      app.Run();
    }
  }
}