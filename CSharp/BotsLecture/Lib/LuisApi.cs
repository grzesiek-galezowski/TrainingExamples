using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;

namespace Lib;

public static class LuisApi
{
  public static async Task<Prediction> GetStructuredOutputFrom(string text)
  {
    var luisKey = await SecretStore.ReadLuisKey();
    var luisAppId = await SecretStore.ReadLuisApp();
    var luisAppUrl = await SecretStore.ReadLuisUrl();
    var credentials = new ApiKeyServiceClientCredentials(luisKey);
    var runtimeClient 
      = new LUISRuntimeClient(credentials) { Endpoint = luisAppUrl };
    var result = await runtimeClient.Prediction.GetSlotPredictionWithHttpMessagesAsync(
      Guid.Parse(luisAppId), 
      "staging", 
      new PredictionRequest(text));

    return result.Body.Prediction;
  }
}