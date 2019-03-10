using BotLogic.Intents;

namespace BotLogic
{
  public class IntentRecognition
  {
    public IIntent From(string text)
    {
      if (text.Contains("catalog"))
      {
        return new WatchCatalogIntent();
      }
      else if (text.Contains("yes"))
      {
        return new YesIntent();
      }
      else if (text.Contains("no"))
      {
        return new NoIntent();
      }
      else if(text.Contains("shop"))
      {
        return new GoShoppingIntent();
      }
      else
      {
        return new InvalidItent();
      }

    }
  }
}