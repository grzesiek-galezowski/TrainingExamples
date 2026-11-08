using System.Text.Json.Serialization;

namespace ServiceSpecificationBuildingBlocks.Zipkin;

public record ZipkinEndpoint(
  [property: JsonPropertyName("serviceName")] string? ServiceName = null,
  [property: JsonPropertyName("ipv4")] string? Ipv4 = null,
  [property: JsonPropertyName("ipv6")] string? Ipv6 = null,
  [property: JsonPropertyName("port")] int? Port = null
);