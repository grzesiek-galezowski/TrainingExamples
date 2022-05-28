using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json.Linq;

namespace Lib;

public static class PlateIntent
{
  public static LicensePlateQueryData GetEntitiesFrom(Prediction data)
  {
    return new LicensePlateQueryData(
      PlateNumber(data),
      State(data));
  }

  private static string? State(Prediction data)
  {
    if (!data.Entities.ContainsKey("State")) return null;
    return ((JArray)data.Entities["State"]).First.ToString();
  }

  private static string? PlateNumber(Prediction data)
  {
    if (!data.Entities.ContainsKey("PlateNumber")) return null;
    return ((JArray)data.Entities["PlateNumber"]).First.ToString().Replace(" ", string.Empty);
  }
}