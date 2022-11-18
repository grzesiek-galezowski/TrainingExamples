using System.Text.Json;

namespace LanguageExtExamples;

static internal class SystemTextJsonOptions
{
  public static JsonSerializerOptions WithLanguageExtExtensions()
  {
    var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    jsonSerializerOptions.Converters.Add(new SeqConverter());
    jsonSerializerOptions.Converters.Add(new ArrConverter());
    return jsonSerializerOptions;
  }
}