using AwesomeAssertions;
using DemoUiServiceSpecification.Automation;
using TddXt.AnyRoot;
using TddXt.AnyRoot.Strings;

namespace DemoUiServiceSpecification.Endpoints.Metrics;

public class MetricsSpecification
{
  [Test]
  public async Task ShouldProvidePrometheusMetrics()
  {
    // GIVEN
    await using var driver = await DemoUiDriver.CreateInstance();
    await driver.Start();

    await driver.Gui.RunSingleScenario(Any.String(), Any.String(), Any.String());

    // WHEN
    var metrics = await driver.RequestMetrics(); //bug metrics response

    // THEN
    // Prometheus format
    metrics.Should().Contain("# HELP");
    metrics.Should().Contain("# TYPE");

    // Service identity
    metrics.Should().Contain("target_info{service_name=\"DemoUi\"");

    // AspNetCore server instrumentation
    metrics.Should().Contain("http_server_request_duration_seconds");

    // HttpClient outbound instrumentation (proves scenario triggered HTTP calls)
    metrics.Should().Contain("http_client_request_duration_seconds");

    // .NET runtime instrumentation
    metrics.Should().Contain("dotnet_gc_collections_total");

    // Blazor component instrumentation (proves UI interaction was measured)
    metrics.Should().Contain("aspnetcore_components_handle_event_duration_seconds");

    // Blazor circuit is active
    metrics.Should().Contain("aspnetcore_components_circuit_active");

    // GUI operations
    metrics.Should().Contain("aspnetcore_components_attribute_name=\"onchange\",aspnetcore_components_type=\"DemoUi.Components.Pages.Home\"");
    metrics.Should().Contain("aspnetcore_components_attribute_name=\"onclick\",aspnetcore_components_type=\"DemoUi.Components.Pages.Home\"");
    metrics.Should().Contain("aspnetcore_components_navigate_total");
    metrics.Should().Contain("aspnetcore_components_route=\"/\"");

  }
}
