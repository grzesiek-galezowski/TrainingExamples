using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation;

internal class WebApp(string databaseName) : WebApplicationFactory<Program>
{
  protected override void ConfigureWebApplicationBuilder(IHostApplicationBuilder builder)
  {
    builder.Configuration.AddInMemoryCollection(
      new Dictionary<string, string?>
      {
        ["DatabaseInfo:ConnectionString"] = SharedMongoInstance.GetConnectionString(databaseName)
      });

    base.ConfigureWebApplicationBuilder(builder);
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.UseTestServer(options => options.PreserveExecutionContext = true);
    base.ConfigureWebHost(builder);
  }
}