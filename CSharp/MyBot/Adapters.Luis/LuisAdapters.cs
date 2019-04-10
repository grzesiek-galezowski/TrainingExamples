using System.IO;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.AI.Luis;

namespace Adapters.Luis
{
  public static class LuisAdapters
  {
    public static LuisRecognizer CreateLuisRecognizer()
    {
      return new LuisRecognizer(
        new LuisApplication(
          File.ReadAllText(@"C:\Users\grzes\Dysk Google\LuisEndpoint.txt")
        ), includeApiResults: true);
    }

    public static LuisUserPhrase CreateLuisUserPhrase(LuisRecognizer luisRecognizer, ITurnContext turnContext)
    {
      return new LuisUserPhrase(turnContext, luisRecognizer);
    }
  }
}