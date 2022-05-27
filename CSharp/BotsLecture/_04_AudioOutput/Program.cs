using Lib;

namespace _04_AudioOutput
{
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

            var response = "The car with " +
                             $"license plate: {queryData.Number} " +
                             $"in state {queryData.State} " +
                             $"is {carMake}";
            await Output(response);
          }
          else
          {
            await Output("Sorry, I don't know what you mean");
          }
        }
        catch(Exception e)
        {
          await Output("Invalid input!");
          Console.WriteLine(e.Message);
        }
      }
    }

    private static async Task Output(string response)
    {
      Console.WriteLine(response);
      await SpeechService.SynthesizeAndRead(response);
    }
  }
}