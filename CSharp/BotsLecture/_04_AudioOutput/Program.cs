using Lib;

namespace _04_AudioOutput;

public static class Program
{
  public static async Task Main(string[] args)
  {
    while (true)
    {
      try
      {
        Console.WriteLine("Speak into your microphone.");
        var text = await SpeechService.MicrophoneSpeechToText();
        Console.WriteLine($"Recognized {text}");
        var data = await LuisApi.GetStructuredOutputFrom(text);

        if (data.TopIntent == "Plate")
        {
          var queryData = PlateIntent.GetEntitiesFrom(data);
          var carMake = FederalDatabase.FindCar(queryData);

          await User.RespondWith("The car with " +
                                 $"license plate: {queryData.Number} " +
                                 $"in state {queryData.State} " +
                                 $"is {carMake}");
        }
        else
        {
          await User.RespondWith("Sorry, I don't know what you mean");
        }
      }
      catch(Exception e)
      {
        await User.RespondWith("Invalid input!");
        Console.WriteLine(e.Message);
      }
    }
  }
}