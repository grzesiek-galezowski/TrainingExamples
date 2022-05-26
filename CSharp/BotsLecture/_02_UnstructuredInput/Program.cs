using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json.Linq;
using ApiKeyServiceClientCredentials = Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.ApiKeyServiceClientCredentials;

public static class Program
{
  public static async Task Main(string[] args)
  {
    //try this:
    //{ "Intent": "Plate", "Number": "ABC1234", "State": "Alabama" }
    //{ "Intent": "Plate", "Number": "ABC1235", "State": "Alabama" }
    //{ "Intent": "Plate", "Number": "ABC12", "State": "New York" }

    while (true)
    {
      try
      {
        var text = Console.ReadLine();
        var data = await GetFromLuis(text);

        if (data.TopIntent == "Plate")
        {
          var queryData = GetEntitiesFrom(data);

          var carMake = FindCar(queryData);

          Console.WriteLine(
            "The car with " +
            $"license plate: {queryData.Number} " +
            $"in state {queryData.State} " +
            $"is {carMake}");
        }
        else
        {
          Console.WriteLine("Sorry, I don't know what you mean");
        }
      }
      catch
      {
        Console.WriteLine("Invalid input!");
      }
    }
  }

  private static LicensePlateQueryData GetEntitiesFrom(Prediction data)
  {
    return new LicensePlateQueryData(
      ((JArray)data.Entities["PlateNumber"]).First.ToString(),
      ((JArray)data.Entities["State"]).First.ToString());
  }

  private static async Task<Prediction> GetFromLuis(string text)
  {
    var luisKey = await File.ReadAllTextAsync("C:\\Users\\HYPERBOOK\\Documents\\__KEYS\\luis.txt");
    var luisAppId = await File.ReadAllTextAsync("C:\\Users\\HYPERBOOK\\Documents\\__KEYS\\luisApp.txt");
    var luisAppUrl = await File.ReadAllTextAsync("C:\\Users\\HYPERBOOK\\Documents\\__KEYS\\luisAppUrl.txt");
    var credentials = new ApiKeyServiceClientCredentials(luisKey);
    var runtimeClient 
      = new LUISRuntimeClient(credentials) { Endpoint = luisAppUrl };
    var result = await runtimeClient.Prediction.GetSlotPredictionWithHttpMessagesAsync(
      Guid.Parse(luisAppId), 
      "staging", 
      new PredictionRequest()
      {
        Query = text
      });

    return result.Body.Prediction;
  }

  private static string FindCar(LicensePlateQueryData queryData)
  {
    return queryData switch
    {
      { State: "Alabama", Number: "ABC1234" } => "Alfa Romeo",
      { State: "Alabama", Number: "ABC1235" } => "Cadillac",
      { State: "New York", Number: "ABC12" } => "Rolls Royce",
      _ => "Unknown"
    };
  }
}

public record LicensePlateQueryData(string Number, string State);