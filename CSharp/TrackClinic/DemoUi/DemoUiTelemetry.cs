using System.Diagnostics;
using Core.NullableReferenceTypesExtensions;

namespace DemoUi;

public class DemoUiTelemetry
{
  public const string ScenariosRootName = "DemoUi.Scenarios";
  public const string ScenarioActivityName = "scenario.execute";
  public const string ScenarioStepActivityName = "step.execute";
  
  private readonly ActivitySource _source = new(ScenariosRootName);

  public DemoUiActivity StartScenarioStepActivity(int stepNumber, string actionName, HttpMethod method, string fullUrl)
  {
    Activity? activity = null;
    try
    {
      activity = _source.StartActivity(ScenarioStepActivityName, ActivityKind.Client).OrThrow();
      activity.SetTag("step.number", stepNumber);
      activity.SetTag("step.actionName", actionName);
      activity.SetTag("step.httpMethod", method.Method);
      activity.SetTag("step.url", fullUrl);
      return new DemoUiActivity(activity);
    }
    catch
    {
      activity?.Dispose();
      throw;
    }
  }

  public DemoUiActivity StartScenarioActivity(string artist, string song1, string song2)
  {
    Activity? activity = null;
    try
    {
      activity = _source.StartActivity(ScenarioActivityName, ActivityKind.Client).OrThrow();
      activity.SetTag("scenario.artist", artist);
      activity.SetTag("scenario.song1", song1);
      activity.SetTag("scenario.song2", song2);
      return new DemoUiActivity(activity);
    }
    catch
    {
      activity?.Dispose();
      throw;
    }
  }
}