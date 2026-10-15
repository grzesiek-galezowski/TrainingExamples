using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using TrackClinic;

namespace TrackClinicServiceSpecification.Automation;

internal class WebApp(string databaseName) : WebApplicationFactory<Program>
{
  //bug async context preserve

  protected override void ConfigureWebApplicationBuilder(IHostApplicationBuilder builder)
  {
    builder.Configuration.AddInMemoryCollection(
      new Dictionary<string, string?>
      {
        ["DatabaseInfo:ConnectionString"] = SharedMongoInstance.GetConnectionString(databaseName)
      });

    base.ConfigureWebApplicationBuilder(builder);
  }
}