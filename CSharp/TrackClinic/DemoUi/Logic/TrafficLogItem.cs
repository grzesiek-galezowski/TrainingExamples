namespace DemoUi.Logic;

public record TrafficLogItem(
  DateTime Timestamp,
  string ScenarioId,
  string HttpMethod,
  string Endpoint,
  int? StatusCode,
  long ElapsedMilliseconds,
  bool Success,
  string? Details
);