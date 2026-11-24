using System.Diagnostics;
using Core.Maybe;
using DemoUi.Dto;
using Flurl.Http;

namespace DemoUi.Logic;

public sealed class ScenarioStep(
  IFlurlClient flurlClient,
  TimeSpan timeout,
  int stepNumber,
  string actionName,
  HttpMethod method,
  string relativeUrl,
  object? content)
{
  public HttpMethod Method { get; } = method;
  public string ActionName { get; } = actionName;
  public int StepNumber { get; } = stepNumber;
  public string RelativeUrl { get; } = relativeUrl;

  public async Task<ScenarioStepResult> Send(
    string token,
    string fullUrl,
    Stopwatch sw,
    CancellationToken cancellationToken)
  {
    var response = await SendRequest(token, cancellationToken);

    var responseStatusCode = response.StatusCode;
    var responseBody = await response.GetStringAsync();
    var responsePreview = GetResponseTextPreview(responseBody);
    var isSuccess = response.ResponseMessage.IsSuccessStatusCode;
    var errorMessage = CreateErrorMessage(isSuccess, responseStatusCode, responsePreview);

    var scenarioStepResult = new ScenarioStepResult(
      StepNumber: StepNumber,
      ActionName: ActionName,
      HttpMethod: Method.Method,
      Url: fullUrl,
      StatusCode: responseStatusCode,
      IsSuccess: isSuccess,
      ElapsedMilliseconds: sw.ElapsedMilliseconds,
      ResponsePreview: responsePreview,
      ErrorMessage: errorMessage.OrElseDefault(),
      Timestamp: DateTime.UtcNow
    );
    return scenarioStepResult;
  }

  private static Maybe<string> CreateErrorMessage(bool isSuccess, int? statusCode,
    string responsePreview)
  {
    string? errorMessage = null;
    if (!isSuccess && string.IsNullOrWhiteSpace(errorMessage))
    {
      errorMessage = $"HTTP {statusCode}: {responsePreview}";
    }

    return errorMessage.ToMaybe();
  }

  private static string GetResponseTextPreview(string responseBody)
  {
    return responseBody.Length > 200 ? responseBody[..200] + "..." : responseBody;
  }

  private async Task<IFlurlResponse> SendRequest(
    string token,
    CancellationToken cancellationToken)
  {
    return await flurlClient
      .Request(RelativeUrl.TrimStart('/'))
      .WithTimeout(timeout)
      .AllowAnyHttpStatus()
      .WithOAuthBearerToken(token)
      .SendAsync(
        Method,
        content is null ? null : JsonContent.Create(content),
        cancellationToken: cancellationToken);
  }
}