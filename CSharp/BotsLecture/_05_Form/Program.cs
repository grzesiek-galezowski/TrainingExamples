using Lib;

namespace _05_Form;

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
          var form = new LicensePlateQueryForm();
          var queryData = PlateIntent.GetEntitiesFrom(data);
          form.UpdateWith(queryData);
          if (form.IsComplete())
          {
            await form.Submit();
          }
          else
          {
            await form.PromptForMissingFields();
          }
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