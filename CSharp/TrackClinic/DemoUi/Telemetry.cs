using System.Diagnostics;
using Core.NullableReferenceTypesExtensions;

namespace DemoUi;

public static class Telemetry
{
  public const string ScenariosRootName = "DemoUi.Scenarios";
  public const string ScenarioActivityName = "scenario.execute";
  public const string ScenarioStepActivityName = "step.execute";
  public static readonly ActivitySource Source = new(ScenariosRootName); //bug static??

  public static Activity StartScenarioStepActivity(int stepNumber, string actionName, HttpMethod method, string fullUrl)
  {
    Activity? activity = null;
    try
    {
      activity = Telemetry.Source.StartActivity(ScenarioStepActivityName, ActivityKind.Client).OrThrow();
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

}