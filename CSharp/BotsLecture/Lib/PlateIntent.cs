using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json.Linq;

namespace Lib;

public static class PlateIntent
{
  public static LicensePlateQueryData GetEntitiesFrom(Prediction data)
  {
    return new LicensePlateQueryData(
      ((JArray)data.Entities["PlateNumber"]).First.ToString().Replace(" ", string.Empty),
      ((JArray)data.Entities["State"]).First.ToString());
  }
}