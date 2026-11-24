using DemoUi.Adapters.Configuration;
using DemoUi.Dto;
using System.Diagnostics;
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
      var totalStopwatch = Stopwatch.StartNew();

      var scenarioSteps = ScenarioStepFactory.CreateScenarioSteps(artist, song1, song2, flurlClient, _timeout);

      var token = await cachingTokenClient.GetBearerTokenAsync(cancellationToken);

      var stepResults = new List<ScenarioStepResult>(scenarioSteps.Count);
      foreach (var scenarioStep in scenarioSteps)
      {
        stepResults.Add(await SendStepAsync(scenarioStep, token, cancellationToken));
      }

      totalStopwatch.Stop();

      return new ScenarioExecutionResultDto(
        ScenarioId: scenarioId,
        ArtistName: artist,
        Song1Title: song1,
        Song2Title: song2,
        Success: stepResults.All(s => s.IsSuccess),
        TotalElapsedMilliseconds: totalStopwatch.ElapsedMilliseconds,
        Timestamp: DateTime.UtcNow,
        Steps: stepResults
      );
    }
    catch (Exception ex)
    {
      activity.RecordError(ex);
      throw;
    }
  }

  private async Task<ScenarioStepResult> SendStepAsync(
    ScenarioStep scenarioStep,
    string token,
    CancellationToken cancellationToken)
  {

    var sw = Stopwatch.StartNew();
    var fullUrl = BuildAbsoluteUrl(scenarioStep.RelativeUrl);
    int? statusCode = null;

    using var activity = telemetry.StartScenarioStepActivity(
      scenarioStep.StepNumber,
      scenarioStep.ActionName,
      scenarioStep.Method,
      fullUrl);

    try
    {
      var scenarioStepResult = await scenarioStep.Send(token, fullUrl, sw, cancellationToken);

      sw.Stop();
      statusCode = scenarioStepResult.StatusCode;

      if (!scenarioStepResult.IsSuccess)
      {
        logger.LogError(
          "Step {StepNumber} {ActionName} failed with status code {StatusCode} for {Url}. Response preview: {ResponsePreview}",
          scenarioStepResult.StepNumber,
          scenarioStepResult.ActionName,
          statusCode,
          scenarioStepResult.Url,
          scenarioStepResult.ResponsePreview);
      }

      return scenarioStepResult;
    }
    catch (Exception ex)
    {
      sw.Stop();
      logger.LogError(
        ex,
        "Failed step {StepNumber} {ActionName} for {Url}",
        scenarioStep.StepNumber,
        scenarioStep.ActionName,
        fullUrl);

      activity.RecordError(ex);

      return new ScenarioStepResult(
        StepNumber: scenarioStep.StepNumber,
        ActionName: scenarioStep.ActionName,
        HttpMethod: scenarioStep.Method.Method,
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

  private string BuildAbsoluteUrl(string relativeUrl)
  {
    return new Uri(new Uri(_baseUrl), relativeUrl).ToString();
  }
}