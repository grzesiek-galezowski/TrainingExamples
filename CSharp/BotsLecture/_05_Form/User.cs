using Lib;

static internal class User
{
  public static async Task RespondWith(string response)
  {
    Console.WriteLine(response);
    await SpeechService.SynthesizeAndRead(response);
  }
}