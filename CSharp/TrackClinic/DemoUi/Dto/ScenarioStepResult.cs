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
);