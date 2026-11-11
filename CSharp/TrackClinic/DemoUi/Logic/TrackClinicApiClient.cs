using DemoUi.Adapters.Configuration;
using DemoUi.Dto;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using Core.Maybe;
using Core.NullableReferenceTypesExtensions;
using DemoUi.Adapters.Keycloak;
using Flurl.Http;

namespace DemoUi.Logic;

//bug split into scenario logic and API client

public class TrackClinicApiClient(IFlurlClient flurlClient, KeycloakTokenClient tokenClient, AppConfig appConfig, ILogger<TrackClinicApiClient> logger)
{
  private readonly string _baseUrl = BuildBaseUrl(appConfig.TrackClinic.BaseUrl);
  private readonly TimeSpan _timeout = TimeSpan.FromSeconds(appConfig.TrackClinic.TimeoutSeconds);
  private readonly BearerTokenClient _bearerTokenClient = new(tokenClient, appConfig.Keycloak); //bug DI
  public string BaseUrl => _baseUrl;

  public async Task<ScenarioExecutionResultDto> ExecuteScenarioAsync(
    string artist,
    string song1,
    string song2,
    CancellationToken cancellationToken = default)
  {
    //bug helper method to start activity and set tags
    using var activity = Telemetry.Source.StartActivity(Telemetry.ScenarioActivityName, ActivityKind.Client).OrThrow();
    activity.SetTag("scenario.artist", artist);
    activity.SetTag("scenario.song1", song1);
    activity.SetTag("scenario.song2", song2);

    try
    {

      var scenarioId = Guid.NewGuid().ToString("N")[..8];
      var steps = new List<ScenarioStepResult>();
      var totalStopwatch = Stopwatch.StartNew();

      var token = (await _bearerTokenClient.TryGetBearerTokenAsync(cancellationToken)).OrThrow();

      // Step 1: Create Song 1
      var step1 = await SendStepAsync(
        stepNumber: 1,
        actionName: $"Create Song 1 ({song1})",
        method: HttpMethod.Post,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs",
        content: new CreateSongRequestDto(song1),
        token: token,
        cancellationToken: cancellationToken);
      steps.Add(step1);

      // Step 2: Create Song 2
      var step2 = await SendStepAsync(
        stepNumber: 2,
        actionName: $"Create Song 2 ({song2})",
        method: HttpMethod.Post,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs",
        content: new CreateSongRequestDto(song2),
        token: token,
        cancellationToken: cancellationToken);
      steps.Add(step2);

      // Step 3: Get Song 1
      var step3 = await SendStepAsync(
        stepNumber: 3,
        actionName: $"Get Song 1 ({song1})",
        method: HttpMethod.Get,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(song1)}",
        content: null,
        token: token,
        cancellationToken: cancellationToken);
      steps.Add(step3);

      // Step 4: Get Song 2
      var step4 = await SendStepAsync(
        stepNumber: 4,
        actionName: $"Get Song 2 ({song2})",
        method: HttpMethod.Get,
        relativeUrl: $"/api/v1/artists/{Uri.EscapeDataString(artist)}/songs/{Uri.EscapeDataString(song2)}",
        content: null,
        token: token,
        cancellationToken: cancellationToken);
      steps.Add(step4);

      totalStopwatch.Stop();
      var allSuccessful = steps.All(s => s.IsSuccess);

      return new ScenarioExecutionResultDto(
        ScenarioId: scenarioId,
        ArtistName: artist,
        Song1Title: song1,
        Song2Title: song2,
        Success: allSuccessful,
        TotalElapsedMilliseconds: totalStopwatch.ElapsedMilliseconds,
        Timestamp: DateTime.UtcNow,
        Steps: steps
      );
    }
    catch (Exception ex)
    {
      // bug decorator/proxy
      activity.SetStatus(ActivityStatusCode.Error, ex.Message);
      activity.AddException(ex);
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

    //bug boilerplate
    using var activity = Telemetry.StartScenarioStepActivity(stepNumber, actionName, method, fullUrl);

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
      activity.SetStatus(ActivityStatusCode.Error, ex.Message);
      activity.AddException(ex);

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

  private static string BuildBaseUrl(Uri baseUrl)
  {
    var baseUrlString = baseUrl.ToString();
    return baseUrlString.EndsWith('/') ? baseUrlString : baseUrlString + "/";
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
