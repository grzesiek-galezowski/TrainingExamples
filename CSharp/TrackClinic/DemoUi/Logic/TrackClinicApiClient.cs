using DemoUi.Adapters.Configuration;
using DemoUi.Dto;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;
using Flurl.Http;

namespace DemoUi.Logic;

//bug split into scenario logic and API client

public class TrackClinicApiClient(
  IFlurlClient flurlClient,
  AppConfig appConfig,
  ILogger<TrackClinicApiClient> logger,
  CachingTokenClient cachingTokenClient,
  DemoUiTelemetry telemetry)
{
  private readonly string _baseUrl = appConfig.TrackClinic.BaseUrl.ToString();
  private readonly TimeSpan _timeout = TimeSpan.FromSeconds(appConfig.TrackClinic.TimeoutSeconds);

  public async Task<ScenarioExecutionResultDto> ExecuteScenarioAsync(
    string artist,
    string song1,
    string song2,
    CancellationToken cancellationToken)
  {
    using var activity = telemetry.StartScenarioActivity(artist, song1, song2);

    try
    {
      var scenarioId = Guid.NewGuid().ToString("N")[..8];
      var steps = new List<ScenarioStepResult>();
      var totalStopwatch = Stopwatch.StartNew();

      var token = (await cachingTokenClient.GetBearerTokenAsync(cancellationToken)).OrThrow();

      // Step 1: Create Song 1
      steps.Add(await SendStepAsync(
        stepNumber: 1,
        actionName: $"Create Song 1 ({song1})",
        method: HttpMethod.Post,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs",
        content: new CreateSongRequestDto(song1),
        token: token,
        cancellationToken: cancellationToken));

      // Step 2: Create Song 2
      steps.Add(await SendStepAsync(
        stepNumber: 2,
        actionName: $"Create Song 2 ({song2})",
        method: HttpMethod.Post,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs",
        content: new CreateSongRequestDto(song2),
        token: token,
        cancellationToken: cancellationToken));

      // Step 3: Get Song 1
      steps.Add(await SendStepAsync(
        stepNumber: 3,
        actionName: $"Get Song 1 ({song1})",
        method: HttpMethod.Get,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(song1)}",
        content: null, //bug convoluted
        token: token,
        cancellationToken: cancellationToken));

      // Step 4: Get Song 2
      steps.Add(await SendStepAsync(
        stepNumber: 4,
        actionName: $"Get Song 2 ({song2})",
        method: HttpMethod.Get,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(song2)}",
        content: null, //bug convoluted
        token: token,
        cancellationToken: cancellationToken));

      totalStopwatch.Stop();

      return new ScenarioExecutionResultDto(
        ScenarioId: scenarioId,
        ArtistName: artist,
        Song1Title: song1,
        Song2Title: song2,
        Success: steps.All(s => s.IsSuccess),
        TotalElapsedMilliseconds: totalStopwatch.ElapsedMilliseconds,
        Timestamp: DateTime.UtcNow,
        Steps: steps
      );
    }
    catch (Exception ex)
    {
      activity.RecordError(ex);
      throw;
    }
  }

  private async Task<ScenarioStepResult> SendStepAsync(
    int stepNumber,
    string actionName,
    HttpMethod method,
    string relativeUrl,
    object? content,
    string token,
    CancellationToken cancellationToken)
  {

    var sw = Stopwatch.StartNew();
    var fullUrl = BuildAbsoluteUrl(relativeUrl);
    int? statusCode = null;

    using var activity = telemetry.StartScenarioStepActivity(stepNumber, actionName, method, fullUrl);

    try
    {
      var response = await CreateRequest(relativeUrl)
        .WithOAuthBearerToken(token)
        .SendAsync(
          method,
          content is null ? null : JsonContent.Create(content),
          cancellationToken: cancellationToken);

      statusCode = response.StatusCode;
      var responseBody = await response.GetStringAsync();
      var responsePreview = GetResponseTextPreview(responseBody);
      var isSuccess = response.ResponseMessage.IsSuccessStatusCode;
      var errorMessage = CreateErrorMessage(isSuccess, statusCode, responsePreview);

      sw.Stop();

      if(!isSuccess)
      {
        logger.LogError("Step {StepNumber} {ActionName} failed with status code {StatusCode} for {Url}. Response preview: {ResponsePreview}", stepNumber, actionName, statusCode, fullUrl, responsePreview);
      }

      return new ScenarioStepResult(
        StepNumber: stepNumber,
        ActionName: actionName,
        HttpMethod: method.Method,
        Url: fullUrl,
        StatusCode: statusCode,
        IsSuccess: isSuccess,
        ElapsedMilliseconds: sw.ElapsedMilliseconds,
        ResponsePreview: responsePreview,
        ErrorMessage: errorMessage.OrElseDefault(),
        Timestamp: DateTime.UtcNow
      );
    }
    catch (Exception ex)
    {
      sw.Stop();
      logger.LogError(ex, "Failed step {StepNumber} {ActionName} for {Url}", stepNumber, actionName, fullUrl);

      // bug boilerplate
      activity.RecordError(ex);

      return new ScenarioStepResult(
        StepNumber: stepNumber,
        ActionName: actionName,
        HttpMethod: method.Method,
        Url: fullUrl,
        StatusCode: statusCode,
        IsSuccess: false,
        ElapsedMilliseconds: sw.ElapsedMilliseconds,
        ResponsePreview: null,
        ErrorMessage: ex.Message,
        Timestamp: DateTime.UtcNow
      );
    }
  }

  //bug add loki

  private IFlurlRequest CreateRequest(string relativeUrl)
  {
    return flurlClient
      .Request(relativeUrl.TrimStart('/'))
      .WithTimeout(_timeout)
      .AllowAnyHttpStatus();
  }

  private string BuildAbsoluteUrl(string relativeUrl)
  {
    return new Uri(new Uri(_baseUrl), relativeUrl).ToString();
  }

  private static string GetResponseTextPreview(string responseBody)
  {
    return responseBody.Length > 200 ? responseBody[..200] + "..." : responseBody;
  }

  private static Maybe<string> CreateErrorMessage(bool isSuccess, [DisallowNull] int? statusCode, string responsePreview)
  {
    string? errorMessage = null;
    if (!isSuccess && string.IsNullOrWhiteSpace(errorMessage))
    {
      errorMessage = $"HTTP {statusCode}: {responsePreview}";
    }

    return errorMessage.ToMaybe();
  }
}
