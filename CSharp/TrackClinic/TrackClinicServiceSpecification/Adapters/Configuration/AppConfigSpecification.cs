using AwesomeAssertions;
using Microsoft.Extensions.Configuration;
using OpenTelemetry.Exporter;
using Serilog.Events;
using TrackClinic.Adapters.Configuration;

namespace TrackClinicServiceSpecification.Adapters.Configuration;

public class AppConfigSpecification //bug refactor
{
  [Test]
  public void ShouldCorrectlyBindAndValidateAllProperties()
  {
    // GIVEN
    var configuration = new ConfigurationManager();
    configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
      ["DatabaseInfo:ConnectionString"] = "mongodb://localhost:27017/db",
      ["Logging:DefaultLevel"] = "Information",
      ["Logging:MicrosoftOverride"] = "Warning",
      ["Keycloak:Authority"] = "http://localhost:8080/realms/test",
      ["Keycloak:MetadataAddress"] = "http://localhost:8080/realms/test/.well-known/openid-configuration",
      ["Keycloak:RequireHttpsMetadata"] = "false",
      ["OpenTelemetry:Endpoint"] = "http://localhost:4318/v1/traces",
      ["OpenTelemetry:Protocol"] = "HttpProtobuf",
      ["OpenTelemetry:ScheduledDelayMilliseconds"] = "100"
    });

    // WHEN
    var appConfig = AppConfig.From(configuration);

    // THEN
    appConfig.DatabaseInfo.ConnectionString.Should().Be("mongodb://localhost:27017/db");
    appConfig.Logging.DefaultLevel.Should().Be(LogEventLevel.Information);
    appConfig.Logging.MicrosoftOverride.Should().Be(LogEventLevel.Warning);
    appConfig.Keycloak.Authority.Should().Be("http://localhost:8080/realms/test");
    appConfig.Keycloak.MetadataAddress.Should().Be("http://localhost:8080/realms/test/.well-known/openid-configuration");
    appConfig.Keycloak.RequireHttpsMetadata.Should().BeFalse();
    appConfig.OpenTelemetry.Endpoint.Should().Be(new Uri("http://localhost:4318/v1/traces"));
    appConfig.OpenTelemetry.Protocol.Should().Be(OtlpExportProtocol.HttpProtobuf);
    appConfig.OpenTelemetry.ScheduledDelayMilliseconds.Should().Be(100);
  }

  [Test]
  public void ShouldThrowWhenOpenTelemetryConfigIsMissing()
  {
    // GIVEN
    var configuration = new ConfigurationManager();
    configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
      ["DatabaseInfo:ConnectionString"] = "mongodb://localhost:27017/db",
      ["Logging:DefaultLevel"] = "Information",
      ["Logging:MicrosoftOverride"] = "Warning",
      ["Keycloak:Authority"] = "http://localhost:8080/realms/test",
      ["Keycloak:MetadataAddress"] = "http://localhost:8080/realms/test/.well-known/openid-configuration"
    });

    // WHEN - THEN
    var act = () => AppConfig.From(configuration);
    act.Should().Throw<ArgumentNullException>();
  }

  [Test]
  public void ShouldThrowWhenOpenTelemetryScheduledDelayIsNonPositive()
  {
    // GIVEN
    var configuration = new ConfigurationManager();
    configuration.AddInMemoryCollection(new Dictionary<string, string?>
    {
      ["DatabaseInfo:ConnectionString"] = "mongodb://localhost:27017/db",
      ["Logging:DefaultLevel"] = "Information",
      ["Logging:MicrosoftOverride"] = "Warning",
      ["Keycloak:Authority"] = "http://localhost:8080/realms/test",
      ["Keycloak:MetadataAddress"] = "http://localhost:8080/realms/test/.well-known/openid-configuration",
      ["OpenTelemetry:Endpoint"] = "http://localhost:4318/v1/traces",
      ["OpenTelemetry:ScheduledDelayMilliseconds"] = "0"
    });

    // WHEN - THEN
    var act = () => AppConfig.From(configuration);
    act.Should().Throw<ArgumentOutOfRangeException>();
  }
}
