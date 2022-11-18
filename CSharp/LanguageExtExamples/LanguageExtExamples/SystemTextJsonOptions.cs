using System.Text.Json;
using LanguageExtExamples.Factories;

namespace LanguageExtExamples;

static internal class SystemTextJsonOptions
{
  public static JsonSerializerOptions WithLanguageExtExtensions()
  {
    var jsonSerializerOptions = new JsonSerializerOptions(JsonSerializerDefaults.Web);
    jsonSerializerOptions.Converters.Add(LanguageExtCollectionConverters.ForSeq());
    jsonSerializerOptions.Converters.Add(LanguageExtCollectionConverters.ForArr());
    return jsonSerializerOptions;
  }
}