namespace DemoUi.Dto;

public record ScenarioStepResult(
    int StepNumber,
    string ActionName,
    string HttpMethod,
    string Url,
    int? StatusCode,
    bool IsSuccess,
    long ElapsedMilliseconds,
    string? ResponsePreview,
    string? ErrorMessage,
    DateTime Timestamp
)
{
  public static ScenarioStepResult Error(Exception ex)
  {
    return new ScenarioStepResult(
      StepNumber: 0,
      ActionName: "Scenario Execution Error",
      HttpMethod: "N/A",
      Url: "",
      StatusCode: null,
      IsSuccess: false,
      ElapsedMilliseconds: 0,
      ResponsePreview: null,
      ErrorMessage: ex.Message,
      Timestamp: DateTime.UtcNow);
  }
}