using System.Text.Json;

namespace _01_NotABot_StructuredInput
{
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

  public record IntentData(string Intent);
  public record LicensePlateQueryData(string Number, string State);
}