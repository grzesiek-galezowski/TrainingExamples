using Newtonsoft.Json;

namespace UnitTestRunnerPackageExercise;

internal class NewtonsoftJsonResultsTextFormat : IResultsTextFormat
{
  public string ApplyTo(TestSetDto dto)
  {
    return JsonConvert.SerializeObject(dto);
  }
}