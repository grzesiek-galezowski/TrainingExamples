using System.Text.Json;

public static class Program
{
  public static void Main(string[] args)
  {
    //try this:
    //{ "Number": "ABC1234", "State": "Alabama" }
    //{ "Number": "ABC1235", "State": "Alabama" }
    //{ "Number": "ABC12", "State": "New York" }

    while (true)
    {
      var text = Console.ReadLine();
      var queryData = JsonSerializer.Deserialize<LicensePlateQueryData>(text);

      var findCar = FindCar(queryData);
      Console.WriteLine(
        $"The car with license plate: {queryData.Number} in state {queryData.State} is " + findCar);

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

public record LicensePlateQueryData(string Number, string State);
