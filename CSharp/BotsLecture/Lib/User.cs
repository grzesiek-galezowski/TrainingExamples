namespace Lib;

public static class User
{
  public static async Task RespondWith(string response)
  {
    Console.WriteLine(response);
    await SpeechService.SynthesizeAndRead(response);
  }
}