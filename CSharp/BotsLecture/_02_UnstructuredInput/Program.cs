using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime;
using Microsoft.Azure.CognitiveServices.Language.LUIS.Runtime.Models;
using Newtonsoft.Json.Linq;

namespace _02_UnstructuredInput
{
  public static class Program
  {
    public static async Task Main(string[] args)
    {
      while (true)
      {
        try
        {
          var text = Console.ReadLine();
          var data = await GetStructuredOutputFrom(text);

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
        catch(Exception e)
        {
          Console.WriteLine("Invalid input!");
          Console.WriteLine(e.Message);
        }
      }
    }

    private static LicensePlateQueryData GetEntitiesFrom(Prediction data)
    {
      return new LicensePlateQueryData(
        ((JArray)data.Entities["PlateNumber"]).First.ToString(),
        ((JArray)data.Entities["State"]).First.ToString());
    }

    private static async Task<Prediction> GetStructuredOutputFrom(string text)
    {
      var luisKey = await File.ReadAllTextAsync($"{Environment.SpecialFolder.MyDocuments}\\__KEYS\\luis.txt");
      var luisAppId = await File.ReadAllTextAsync($"{Environment.SpecialFolder.MyDocuments}\\__KEYS\\luisApp.txt");
      var luisAppUrl = await File.ReadAllTextAsync($"{Environment.SpecialFolder.MyDocuments}\\__KEYS\\luisAppUrl.txt");
      var credentials = new ApiKeyServiceClientCredentials(luisKey);
      var runtimeClient 
        = new LUISRuntimeClient(credentials) { Endpoint = luisAppUrl };
      var result = await runtimeClient.Prediction.GetSlotPredictionWithHttpMessagesAsync(
        Guid.Parse(luisAppId), 
        "staging", 
        new PredictionRequest(text));

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
}