using Lib;

namespace _02_UnstructuredInput;

public static class Program
{
  public static async Task Main(string[] args)
  {
    while (true)
    {
      try
      {
        var text = Console.ReadLine();
        var data = await LuisApi.GetStructuredOutputFrom(text);

        if (data.TopIntent == "Plate")
        {
          var queryData = PlateIntent.GetEntitiesFrom(data);

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