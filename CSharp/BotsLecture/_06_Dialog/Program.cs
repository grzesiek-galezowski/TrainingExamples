using Lib;

namespace _06_Dialog;

public static class Program
{
  public static async Task Main(string[] args)
  {
    var dialog = new Dialog();
    await dialog.Initialize();
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
          await dialog.OnCheckLicensePlateIntent(data);
        }
        else if (data.TopIntent == "ContextFreePlateData")
        {
          await dialog.OnContextFreePlateDataIntent(data);
        }
        else
        {
          await dialog.OnUnknownIntent();
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