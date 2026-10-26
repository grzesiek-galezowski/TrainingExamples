using System.Collections.Immutable;
using System.Text.Json.Serialization;

namespace TrackClinicServiceSpecification.Automation.Zipkin;

public record ZipkinSpan( //bug why so many nulls?
  [property: JsonPropertyName("traceId")] string TraceId,
  [property: JsonPropertyName("id")] string Id,
  [property: JsonPropertyName("parentId")] string? ParentId = null,
  [property: JsonPropertyName("name")] string? Name = null,
  [property: JsonPropertyName("kind")] string? Kind = null,
  [property: JsonPropertyName("timestamp")] long? Timestamp = null,
  [property: JsonPropertyName("duration")] long? Duration = null,
  [property: JsonPropertyName("debug")] bool? Debug = null,
  [property: JsonPropertyName("shared")] bool? Shared = null,
  [property: JsonPropertyName("localEndpoint")] ZipkinEndpoint? LocalEndpoint = null,
  [property: JsonPropertyName("remoteEndpoint")] ZipkinEndpoint? RemoteEndpoint = null,
  [property: JsonPropertyName("annotations")] ImmutableList<ZipkinAnnotation>? Annotations = null,
  [property: JsonPropertyName("tags")] ImmutableDictionary<string, string>? Tags = null
);