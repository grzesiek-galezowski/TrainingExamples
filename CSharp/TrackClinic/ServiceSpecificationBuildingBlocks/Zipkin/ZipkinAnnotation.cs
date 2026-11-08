using System.Text.Json.Serialization;

namespace ServiceSpecificationBuildingBlocks.Zipkin;

public record ZipkinAnnotation(
  [property: JsonPropertyName("timestamp")] long Timestamp,
  [property: JsonPropertyName("value")] string Value
);