using System.Text.Json;
using Lib;

namespace _01_NotABot_StructuredInput;

public static class Program
{
  public static void Main(string[] args)
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
        var indentData = JsonSerializer.Deserialize<IntentData>(text);

        if (indentData is { Intent: "Plate" })
        {
          var queryData = JsonSerializer.Deserialize<LicensePlateQueryData>(text);

          var carMake = FederalDatabase.FindCar(queryData);

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
}

public record IntentData(string Intent);