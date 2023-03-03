using System.Text.Json;

namespace UnitTestRunnerPackageExercise;

public class SystemTextJsonResultsTextFormat : IResultsTextFormat
{
  public string ApplyTo(TestSetDto dto)
  {
    return JsonSerializer.Serialize(dto);
  }
}