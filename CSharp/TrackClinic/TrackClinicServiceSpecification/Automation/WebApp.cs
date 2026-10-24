using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using TrackClinic;
using TrackClinicServiceSpecification.Automation.Keycloak;

namespace TrackClinicServiceSpecification.Automation;

internal class WebApp(string databaseName, string keycloakRealmName) : WebApplicationFactory<Program>
{
  protected override void ConfigureWebApplicationBuilder(IHostApplicationBuilder builder)
  {
    builder.Configuration.AddInMemoryCollection(
      new Dictionary<string, string?>
      {
        ["DatabaseInfo:ConnectionString"] = SharedMongoInstance.GetConnectionString(databaseName),
        ["Keycloak:Authority"] = SharedKeycloakInstance.GetAuthority(keycloakRealmName),
        ["Keycloak:MetadataAddress"] = SharedKeycloakInstance.GetMetadataAddress(keycloakRealmName),
        ["Keycloak:RequireHttpsMetadata"] = "false"
      });
    base.ConfigureWebApplicationBuilder(builder);
  }

  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.UseTestServer(options => options.PreserveExecutionContext = true);
    base.ConfigureWebHost(builder);
  }

  protected override IHost CreateHost(IHostBuilder builder)
  {
    builder.UseSerilog(LoggingConfigurations.ConfigureServiceLogging(
      new LoggerConfiguration(), LogEventLevel.Information, LogEventLevel.Information)
      .CreateLogger(), dispose: true);
    var host = base.CreateHost(builder);
    return host;
  }
}