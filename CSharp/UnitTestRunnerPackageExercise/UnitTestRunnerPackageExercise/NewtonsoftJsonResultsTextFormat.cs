using Newtonsoft.Json;

namespace UnitTestRunnerPackageExercise;

public class NewtonsoftJsonResultsTextFormat : IResultsTextFormat
{
  public string ApplyTo(TestSetDto dto)
  {
    return JsonConvert.SerializeObject(dto);
  }
}