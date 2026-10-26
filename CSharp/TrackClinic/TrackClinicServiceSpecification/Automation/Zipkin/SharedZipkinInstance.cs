using DotNet.Testcontainers.Builders;
using DotNet.Testcontainers.Containers;
using DotNet.Testcontainers.Networks;

namespace TrackClinicServiceSpecification.Automation.Zipkin;

public static class SharedZipkinInstance
{
  private const int ZipkinPort = 9411;
  private const int OtlpGrpcPort = 4317;
  private const int OtlpHttpPort = 4318;

  private static readonly INetwork Network = new NetworkBuilder().Build();

  private static readonly IContainer ZipkinContainer = new ContainerBuilder("openzipkin/zipkin:3.6")
    .WithNetwork(Network)
    .WithNetworkAliases("zipkin")
    .WithPortBinding(ZipkinPort, true)
    .WithWaitStrategy(
      Wait.ForUnixContainer().UntilHttpRequestIsSucceeded(r => r.ForPort(ZipkinPort).ForPath("/health")))
    .Build();

  private static readonly IContainer OtelCollectorContainer = new ContainerBuilder("otel/opentelemetry-collector:latest")
    .WithNetwork(Network)
    .WithPortBinding(OtlpGrpcPort, true)
    .WithPortBinding(OtlpHttpPort, true)
    .WithEnvironment("OTEL_CONFIG", @"receivers:
  otlp:
    protocols:
      grpc:
        endpoint: 0.0.0.0:4317
      http:
        endpoint: 0.0.0.0:4318
exporters:
  zipkin:
    endpoint: http://zipkin:9411/api/v2/spans
    format: proto
service:
  pipelines:
    traces:
      receivers: [otlp]
      exporters: [zipkin]")
    .WithCommand("--config=env:OTEL_CONFIG")
    .WithWaitStrategy(
      Wait.ForUnixContainer().UntilMessageIsLogged(".*Everything is ready.*"))
    .Build();

  public static async Task Start()
  {
    await Network.CreateAsync();
    await ZipkinContainer.StartAsync();
    await OtelCollectorContainer.StartAsync();
  }

  public static async Task Stop()
  {
    await OtelCollectorContainer.StopAsync();
    await ZipkinContainer.StopAsync();
    await Network.DeleteAsync();
  }

  public static string GetBaseAddress() =>
    $"http://{ZipkinContainer.Hostname}:{ZipkinContainer.GetMappedPublicPort(ZipkinPort)}/";

  public static string GetOtlpHttpEndpoint() =>
    $"http://{OtelCollectorContainer.Hostname}:{OtelCollectorContainer.GetMappedPublicPort(OtlpHttpPort)}/v1/traces";
}