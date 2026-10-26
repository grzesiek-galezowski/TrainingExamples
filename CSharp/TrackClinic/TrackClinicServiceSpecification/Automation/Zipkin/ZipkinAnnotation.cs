using System.Text.Json.Serialization;

namespace TrackClinicServiceSpecification.Automation.Zipkin;

public record ZipkinAnnotation(
  [property: JsonPropertyName("timestamp")] long Timestamp,
  [property: JsonPropertyName("value")] string Value
);