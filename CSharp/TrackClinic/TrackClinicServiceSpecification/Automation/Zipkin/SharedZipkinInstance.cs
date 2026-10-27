using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;

namespace TrackClinicServiceSpecification.Automation.Zipkin;

public static class SharedZipkinInstance
{
  private const int ZipkinPort = 9411;

  private static readonly IContainer ZipkinContainer = new ContainerBuilder("ghcr.io/openzipkin-contrib/zipkin-otel:0.3.0")
    .WithPortBinding(ZipkinPort, true)
    .WithWaitStrategy(
      Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(ZipkinPort).ForPath("/health")))
    .Build();

  public static async Task Start(CancellationToken cancellationToken)
  {
    await ZipkinContainer.StartAsync(cancellationToken);
  }

  public static async Task Stop(CancellationToken cancellationToken)
  {
    await ZipkinContainer.StopAsync(cancellationToken);
    await ZipkinContainer.DisposeAsync();
  }

  public static string GetBaseAddress() =>
    $"http://{ZipkinContainer.Hostname}:{ZipkinContainer.GetMappedPublicPort(ZipkinPort)}/";

  public static string GetOtlpHttpEndpoint() =>
    $"http://{ZipkinContainer.Hostname}:{ZipkinContainer.GetMappedPublicPort(ZipkinPort)}/v1/traces";
}