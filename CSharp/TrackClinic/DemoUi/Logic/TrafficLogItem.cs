using DemoUi.Dto;

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
)
{
  public static TrafficLogItem From(ScenarioExecutionResultDto result, ScenarioStepResult step)
  {
    return new TrafficLogItem(
      Timestamp: step.Timestamp,
      ScenarioId: result.ScenarioId,
      HttpMethod: step.HttpMethod,
      Endpoint: step.Url,
      StatusCode: step.StatusCode,
      ElapsedMilliseconds: step.ElapsedMilliseconds,
      Success: step.IsSuccess,
      Details: step.IsSuccess ? step.ActionName : $"{step.ActionName}: {step.ErrorMessage}"
    );
  }
}