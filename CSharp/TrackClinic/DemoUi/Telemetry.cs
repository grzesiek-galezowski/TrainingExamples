using System.Diagnostics;
using Core.NullableReferenceTypesExtensions;

namespace DemoUi;

public class Telemetry
{
  public const string ScenariosRootName = "DemoUi.Scenarios";
  public const string ScenarioActivityName = "scenario.execute";
  public const string ScenarioStepActivityName = "step.execute";
  
  private static readonly ActivitySource Source = new(ScenariosRootName); //bug static??

  public static Activity StartScenarioStepActivity(int stepNumber, string actionName, HttpMethod method, string fullUrl)
  {
    Activity? activity = null;
    try
    {
      activity = Source.StartActivity(ScenarioStepActivityName, ActivityKind.Client).OrThrow();
      activity.SetTag("step.number", stepNumber);
      activity.SetTag("step.actionName", actionName);
      activity.SetTag("step.httpMethod", method.Method);
      activity.SetTag("step.url", fullUrl);
      return activity;
    }
    catch
    {
      activity?.Dispose();
      throw;
    }
  }

  public static Activity StartScenarioActivity(string artist, string song1, string song2)
  {
    Activity? activity = null;
    try
    {
      activity = Telemetry.Source.StartActivity(Telemetry.ScenarioActivityName, ActivityKind.Client).OrThrow();
      activity.SetTag("scenario.artist", artist);
      activity.SetTag("scenario.song1", song1);
      activity.SetTag("scenario.song2", song2);
      return activity;
    }
    catch
    {
      activity?.Dispose();
      throw;
    }
  }
}